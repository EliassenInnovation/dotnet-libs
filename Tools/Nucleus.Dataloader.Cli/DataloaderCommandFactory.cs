using Eliassen.MongoDB.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace Nucleus.Dataloader.Cli
{
    public class DataloaderCommandFactory : IDataloaderCommandFactory
    {
        private readonly ILogger _log;
        private readonly IEnumerable<IDataloaderCommand> _commands;

        public DataloaderCommandFactory(
            ILogger<DataloaderCommandFactory> log,
            IEnumerable<IDataloaderCommand> commands

            , IOptions<DataLoaderSettings> dataSettings
            , IOptions<DefaultMongoDatabaseSettings> mongoSettings
            )
        {
            _log = log;
            _commands = commands;

            log.LogInformation($"{nameof(DataLoaderSettings)}: {{{nameof(DataLoaderSettings)}}}", dataSettings.Value);
            log.LogInformation($"{nameof(DefaultMongoDatabaseSettings)}: {{{nameof(DefaultMongoDatabaseSettings)}}}", mongoSettings.Value);
        }

        public async Task ExecuteAsync(DataLoaderActions action, Type type, object database, CancellationToken cancellationToken)
        {
            var exceptions = new List<Exception>();
            foreach (var collection in type.GetProperties())
            {
                try
                {
                    var elementType = collection.PropertyType.GetGenericArguments()[0];
                    var setType = elementType.MakeArrayType();

                    //TODO: need to simplify this name logic with that on the MongoDispatchProxy
                    var collectionName = collection.GetCustomAttributes<CollectionNameAttribute>().FirstOrDefault()?.CollectionName ??
                        elementType.GetCustomAttributes<CollectionNameAttribute>().FirstOrDefault()?.CollectionName ??
                        elementType.Name;
                    var data = collection.GetValue(database) ?? throw new NotSupportedException($"{collection} has no value on {database}");

                    var commands = from cmd in _commands
                                   where (cmd.Action & action) != 0
                                   orderby cmd.Priority
                                   select cmd;

                    foreach (var command in commands)
                    {
                        if (cancellationToken.IsCancellationRequested) return;
                        await command.ExecuteAsync(collectionName, elementType, data, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError($"{{{nameof(collection)}}}::{{{nameof(database)}}} -> {{{nameof(ex.Message)}}}", collection, database, ex.Message);
                    _log.LogDebug($"{{{nameof(collection)}}}::{{{nameof(database)}}} -> {{{nameof(Exception)}}}", collection, database, ex);
                    exceptions.Add(ex);
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

    }
}