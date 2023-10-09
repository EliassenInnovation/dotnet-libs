using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Eliassen.System.Text.Json;

public class BsonTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var info = base.GetTypeInfo(type, options);
        var idConvention =
            (
            type.Name.EndsWith("Model") ? type.Name[..^5] :
            type.Name.EndsWith("Collection") ? type.Name[..^10] :
            type.Name
            ) + "Id";

        foreach (var prop in info.Properties)
        {
            var hasIdAttribute = prop.AttributeProvider?.GetCustomAttributes(true).Any(a =>
                a is KeyAttribute ||
                a.GetType().Name == "BsonRepresentationAttribute" //TODO: consider making this injectable so it can be extended
                ) ?? false;

            if (string.Equals(prop.Name, idConvention, StringComparison.InvariantCultureIgnoreCase))
            {
                prop.Name = "_id";
                prop.CustomConverter = new BsonIdConverter();
            }
            else if (hasIdAttribute)
            {
                prop.CustomConverter = new BsonIdConverter();
            }
            else if (new[] {
                typeof(DateTime),
                typeof(DateTime?),
                typeof(DateTimeOffset),
                typeof(DateTimeOffset?),
            }.Contains(prop.PropertyType))
            {
                prop.CustomConverter = new BsonDateTimeOffsetConverter();
            }
        }

        return info;
    }
}
