using Eliassen.System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Nucleus.Dataloader.Cli;

public class DataloaderImportCommand : IDataloaderCommand
{
    private readonly ILogger _log;
    private readonly DataLoaderSettings _settings;
    private readonly ISerializer _serializer;

    public DataloaderImportCommand(
        ILogger<DataloaderImportCommand> log,
        IOptions<DataLoaderSettings> settings,
        IBsonSerializer serializer
        )
    {
        _log = log;
        _settings = settings.Value;
        _serializer = serializer;
    }

    public DataLoaderActions Action => DataLoaderActions.Import;
    public int Priority => 10;

    public async Task ExecuteAsync(string collectionName, Type elementType, object data, CancellationToken cancellationToken)
    {
        var sourceFile = Path.Combine(_settings.SourcePath, $"{collectionName}.json");

        _log.LogInformation(
            $"Importing: {{{nameof(collectionName)}}} from \"{{{nameof(sourceFile)}}}\"",
            collectionName,
            sourceFile
            );

        using var fileStream = File.OpenRead(sourceFile);

        var result = await _serializer.DeserializeAsync(fileStream, elementType.MakeArrayType(), cancellationToken);

        var collectionType = typeof(IMongoCollection<>).MakeGenericType(elementType);


        var idConvention =
            (
            elementType.Name.EndsWith("Model") ? elementType.Name[..^5] :
            elementType.Name.EndsWith("Collection") ? elementType.Name[..^10] :
            elementType.Name
            ) + "Id";

        var idProperty =
            elementType.GetProperties().FirstOrDefault(e => e.GetCustomAttribute<BsonIdAttribute>() != null)
            ?? elementType.GetProperties().FirstOrDefault(e => e.GetCustomAttribute<KeyAttribute>() != null)
            ?? elementType.GetProperties().FirstOrDefault(e => string.Equals(e.Name, idConvention, StringComparison.InvariantCultureIgnoreCase))
            ?? throw new NotSupportedException();

        var parameter = Expression.Parameter(elementType, "e");
        var idSelector = Expression.Lambda(Expression.Property(parameter, idProperty), parameter);

        var castMethod = typeof(Queryable).GetMethod(nameof(Queryable.Cast))?.MakeGenericMethod(elementType);

        var collectionQuerableParameterType = Type.MakeGenericSignatureType(typeof(IMongoCollection<>), Type.MakeGenericMethodParameter(0));
        var collectionQueryableMethod = typeof(IMongoCollectionExtensions)
            .GetMethod(
                name: nameof(IMongoCollectionExtensions.AsQueryable),
                genericParameterCount: 1,
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                binder: null,
                types: new[] { collectionQuerableParameterType, typeof(AggregateOptions) },
                modifiers: null
                )
            ?.MakeGenericMethod(elementType) ??
            throw new NotSupportedException($"{nameof(IMongoCollectionExtensions.AsQueryable)} for {typeof(IMongoCollection<>).MakeGenericType(elementType)} not found")
            ;

        var selectorExpressionType = typeof(Expression<>).MakeGenericType(
            Type.MakeGenericSignatureType(typeof(Func<,>),
                Type.MakeGenericMethodParameter(0),
                Type.MakeGenericMethodParameter(1))
            );
        var queryableSelectorOpen = typeof(Queryable).GetMethod(nameof(Queryable.Select), 2, new[]
        {
                Type.MakeGenericSignatureType(typeof(IQueryable<>), Type.MakeGenericMethodParameter(0)),
                selectorExpressionType
            });
        var closedArrayQueryMethod = queryableSelectorOpen?.MakeGenericMethod(elementType, idProperty.PropertyType);


        var enumerableParameterType = Type.MakeGenericSignatureType(typeof(IEnumerable<>), Type.MakeGenericMethodParameter(0));
        var toArrayMethod = typeof(Enumerable)
            .GetMethod(
                name: nameof(Enumerable.ToArray),
                genericParameterCount: 1,
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                binder: null,
                types: new[] { enumerableParameterType },
                modifiers: null
                );
        var closedIdToArrayMethod = toArrayMethod?.MakeGenericMethod(idProperty.PropertyType);
        var closedElementToArrayMethod = toArrayMethod?.MakeGenericMethod(elementType);

        var openExceptMethod = typeof(Enumerable)
            .GetMethod(
                name: nameof(Enumerable.Except),
                genericParameterCount: 1,
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                binder: null,
                types: new[] { enumerableParameterType, enumerableParameterType },
                modifiers: null
                );
        var closedExceptMethod = openExceptMethod?.MakeGenericMethod(idProperty.PropertyType);

        var keyLookupTypeFunc = Type.MakeGenericSignatureType(
            typeof(Func<,>),
            Type.MakeGenericMethodParameter(0),
            Type.MakeGenericMethodParameter(1)
            );
        var expressionKeyLookupType = typeof(Expression<>).MakeGenericType(keyLookupTypeFunc);
        var queryableParameterType0 = Type.MakeGenericSignatureType(typeof(IQueryable<>), Type.MakeGenericMethodParameter(0));
        var queryableParameterType1 = Type.MakeGenericSignatureType(typeof(IQueryable<>), Type.MakeGenericMethodParameter(1));
        var openExceptByMethod = typeof(Queryable)
            .GetMethod(
                name: nameof(Queryable.ExceptBy),
                genericParameterCount: 2,
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                binder: null,
                types: new[] { queryableParameterType0, queryableParameterType1, expressionKeyLookupType },
                modifiers: null
                );
        var closedExceptByMethod = openExceptByMethod?.MakeGenericMethod(elementType, idProperty.PropertyType);

        var insertManyMethod = collectionType
            .GetMethod(
                name: "InsertMany",
                bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                types: new[] {
                        typeof(IEnumerable<>).MakeGenericType(elementType),
                        typeof(InsertManyOptions),
                        typeof(CancellationToken),
                    },
                modifiers: null
                );

        if (data.GetType().IsAssignableTo(collectionType) && result is object[] array)
        {
            var queryableArray = castMethod?.Invoke(null, new object[] { array.AsQueryable() });

            var queryableCollection = collectionQueryableMethod.Invoke(null, new object?[] { data, null });
            var collectionIdsQuery = closedArrayQueryMethod?.Invoke(null, new object?[] { queryableCollection, idSelector });
            var collectionIds = closedIdToArrayMethod?.Invoke(null, new object?[] { collectionIdsQuery });

            var missingElements = closedExceptByMethod?.Invoke(null, new object?[] { queryableArray, collectionIds, idSelector });
            var missingElementsArray = (Array?)closedElementToArrayMethod?.Invoke(null, new object?[] { missingElements });

            if (missingElementsArray?.Length == 0)
            {
                _log.LogInformation(
                    $"Imported: no records into {{{nameof(collectionName)}}} from \"{{{nameof(sourceFile)}}}\"",
                    collectionName,
                    sourceFile
                    );
            }
            else
            {
                insertManyMethod?.Invoke(data, new object?[] { missingElementsArray, null, CancellationToken.None });

                _log.LogInformation(
                    $"Imported: #{{count}} into {{{nameof(collectionName)}}} from \"{{{nameof(sourceFile)}}}\"",
                    missingElementsArray?.Length ?? 0,
                    collectionName,
                    sourceFile
                    );
            }
        }
    }
}