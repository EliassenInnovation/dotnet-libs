﻿using Eliassen.System.Text.Json;
using System.Text.Json.Serialization;

namespace Eliassen.System.Tests.Text.Json.TestTargets;

public class TestIdProperty
{
    [JsonPropertyName("_id")]
    [JsonConverter(typeof(BsonIdConverter))]
    public string? ProjectId { get; set; }
}
