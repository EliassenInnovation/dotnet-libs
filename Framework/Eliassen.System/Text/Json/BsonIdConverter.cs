using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eliassen.System.Text.Json;

/// <summary>
/// This type converter for System.Text.Json to support BSON ObjectID to JSON safe export/import
/// </summary>
public class BsonIdConverter : JsonConverter<string>
{
    /// <inheritdoc />
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var type = reader.TokenType;

        if (type == JsonTokenType.StartObject)
        {
            if (reader.Read() && reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "$oid")
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    var ret = reader.GetString();
                    reader.Read();
                    return ret;
                }
            }
        }
        else if (type == JsonTokenType.String)
        {
            return reader.GetString();
        }

        throw new NotSupportedException($"element of type {type} is not supported");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("$oid", value);
        writer.WriteEndObject();
    }
}
