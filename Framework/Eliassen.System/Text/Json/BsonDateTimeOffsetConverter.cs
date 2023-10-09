using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eliassen.System.Text.Json;

/// <summary>
/// System.Text.Json converter to support BsonDatetimeOffset
/// </summary>
public class BsonDateTimeOffsetConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert) =>
        new[] {
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?),
            typeof(DateTime),
            typeof(DateTime?),
        }.Contains(typeToConvert);

    /// <inheritdoc/>
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var type = reader.TokenType;

        if (type == JsonTokenType.StartObject)
        {
            if (reader.Read() && reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "$date")
            {
                if (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTimeOffset(out var dt))
                    {
                        reader.Read();
                        return dt;
                    }
                }
            }
        }
        else if (type == JsonTokenType.String && reader.TryGetDateTimeOffset(out var dt))
        {
            return dt;
        }
        else if (type == JsonTokenType.StartArray)
        {
            DateTimeOffset? dateTime = null;
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var value = reader.GetString();
                    if (DateTimeOffset.TryParse(value, out dt))
                    {
                        dateTime = dt;
                    }
                    else if (long.TryParse(value, out var ticks))
                    {
                        dateTime = new DateTime(ticks, DateTimeKind.Utc);
                    }
                }
                else if (reader.TokenType == JsonTokenType.Number && dateTime.HasValue)
                {
                    dateTime = new DateTimeOffset(dateTime.Value.DateTime, TimeSpan.FromMinutes(reader.GetInt32()));
                }
            }
            if (dateTime.HasValue)
                return dateTime.Value;
        }

        throw new NotSupportedException($"element of type {type} is not supported");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value is DateTimeOffset dto)
        {
            writer.WriteString("$date", dto);
        }
        else if (value is DateTime dt)
        {
            writer.WriteString("$date", dt);
        }
        else
        {
            throw new NotSupportedException();
        }
        writer.WriteEndObject();
    }
}