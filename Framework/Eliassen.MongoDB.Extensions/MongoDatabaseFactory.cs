using Eliassen.System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace Eliassen.MongoDB.Extensions;

/// <inheritdoc/>
public class MongoDatabaseFactory : IMongoDatabaseFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <inheritdoc/>
    public MongoDatabaseFactory(
        IServiceProvider serviceProvider
        )
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public IMongoDatabase Create<TSettings>()
        where TSettings : class, IMongoSettings
    {
        var settings = _serviceProvider.GetRequiredService<IOptions<TSettings>>();
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        return database;
    }

    /// <inheritdoc/>
    public TDatabase Create<TDatabase, TSettings>()
        where TSettings : class, IMongoSettings
    {
        var settings = _serviceProvider.GetRequiredService<IOptions<TSettings>>();
        var jsonSerializer = _serviceProvider.GetRequiredService<IJsonSerializer>();
        var database = Create<TSettings>();
        var proxy = MongoDispatchProxy.Create<TDatabase>(database, settings.Value, jsonSerializer);
        return proxy;
    }

    static MongoDatabaseFactory()
    {
        BsonSerializer.RegisterSerializer(
            new DateTimeOffsetSerializer(global::MongoDB.Bson.BsonType.DateTime)
            );

        ConventionRegistry.Register("Camel case convention", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new BsonObjectIdConvention(),
        }, t => true);

    }
}