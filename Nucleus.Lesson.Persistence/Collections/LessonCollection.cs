using Eliassen.System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace Nucleus.Lesson.Persistence.Collections
{
    [BsonIgnoreExtraElements]
    public class LessonCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        [JsonConverter(typeof(BsonIdConverter))]
        public string? LessonId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("lessonScheduleId")]
        [JsonConverter(typeof(BsonIdConverter))]
        public string? LessonScheduleId { get; set; }

        [BsonElement("lessonDateTime")]
        [JsonConverter(typeof(BsonDateConverter))]
        public DateTimeOffset? LessonDateTime { get; set; }

        [BsonElement("student")]
        public string? Student { get; set; }

        [BsonElement("notes")]
        public string? Notes { get; set; }

        [BsonElement("paymentStatus")]
        public string? PaymentStatus { get; set; }

    }
}