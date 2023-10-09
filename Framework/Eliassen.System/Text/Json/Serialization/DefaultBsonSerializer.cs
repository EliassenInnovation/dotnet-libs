using System.Text.Json;

namespace Eliassen.System.Text.Json.Serialization;

/// <summary>
/// Default serializer for JSON
/// </summary>
public class DefaultBsonSerializer : DefaultJsonSerializer, IBsonSerializer
{
    /// <inheritdoc />
    public const string DefaultContentType = "application/json";
    /// <inheritdoc />
    public string ContentType => DefaultContentType;

    /// <inheritdoc />
    public DefaultBsonSerializer(
        JsonSerializerOptions? options = null
        )
    {
        _options.TypeInfoResolver = new BsonTypeInfoResolver();
    }

}