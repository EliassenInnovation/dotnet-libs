using Eliassen.System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Eliassen.System.Tests.Text.Json.TestTargets;

public class TestDateProperty
{
    [JsonConverter(typeof(BsonDateTimeOffsetConverter))]
    public DateTimeOffset? Nullable { get; set; }

    [JsonConverter(typeof(BsonDateTimeOffsetConverter))]
    public DateTimeOffset Value { get; set; }
}
