﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Eliassen.System.Text.Json.Serialization;

/// <summary>
/// Default serializer for JSON.
/// </summary>
public class DefaultJsonSerializer : IJsonSerializer
{
    /// <summary>
    /// Gets the default JSON serializer options.
    /// </summary>
    public static JsonSerializerOptions DefaultOptions => new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,

        IncludeFields = false,
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = false, // Note: this being false enables anonymous serialization.

        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

        Converters =
        {
            new DictionaryStringObjectJsonConverter(),
            new ConfigurationJsonConverter<IConfiguration>(),
            new ConfigurationJsonConverter<IConfigurationSection>(),
        },
    };

    /// <summary>
    /// Gets the default content type for JSON.
    /// </summary>
    public const string DefaultContentType = "text/json";

    /// <inheritdoc/>
    public string ContentType => DefaultContentType;

    /// <summary>
    /// The JSON serializer options.
    /// </summary>
    protected readonly JsonSerializerOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultJsonSerializer"/> class.
    /// </summary>
    /// <param name="options">Optional JSON serializer options.</param>
    public DefaultJsonSerializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? DefaultOptions;
    }

    /// <inheritdoc/>
    public virtual string Serialize<T>(T obj) => Serialize(obj, typeof(T));

    /// <inheritdoc/>
    public virtual string Serialize(object? obj, Type type) =>
        JsonSerializer.Serialize(obj, (type == typeof(object) ? obj?.GetType() : null) ?? type, _options);

    /// <inheritdoc/>
    public virtual Task SerializeAsync<T>(T obj, Stream stream, CancellationToken cancellationToken = default) =>
        SerializeAsync(obj, typeof(T), stream, cancellationToken);

    /// <inheritdoc/>
    public virtual async Task SerializeAsync(object? obj, Type type, Stream stream, CancellationToken cancellationToken = default) =>
        await JsonSerializer.SerializeAsync(stream, obj, _options, cancellationToken);

    /// <inheritdoc/>
    public virtual T? Deserialize<T>(Stream stream) => (T?)Deserialize(stream, typeof(T));

    /// <inheritdoc/>
    public virtual object? Deserialize(Stream stream, Type type) =>
        JsonSerializer.Deserialize(stream, type, _options);

    /// <inheritdoc/>
    public virtual async ValueTask<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
        (T?)await DeserializeAsync(stream, typeof(T), cancellationToken);

    /// <inheritdoc/>
    public virtual async ValueTask<object?> DeserializeAsync(Stream stream, Type type, CancellationToken cancellationToken = default) =>
        await JsonSerializer.DeserializeAsync(stream, type, _options, cancellationToken);

    /// <inheritdoc/>
    public virtual T? Deserialize<T>(string input) => (T?)Deserialize(input, typeof(T));

    /// <inheritdoc/>
    public virtual object? Deserialize(string input, Type type) =>
        JsonSerializer.Deserialize(input, type, _options);

    /// <summary>
    /// Use the configured property naming policy to change the provided value.
    /// </summary>
    /// <param name="propertyName">The property name to convert.</param>
    /// <returns>The converted property name.</returns>
    public string AsPropertyName(string propertyName) =>
        (_options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase).ConvertName(propertyName);
}
