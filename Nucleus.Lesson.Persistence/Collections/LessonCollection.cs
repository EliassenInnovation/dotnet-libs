using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Lesson.Persistence.Collections
{
    [BsonIgnoreExtraElements]
    public class LessonCollection
    {
        [Key]
        public string? LessonId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? LessonScheduleId { get; set; }
        public DateTimeOffset? LessonDateTime { get; set; }
        public string? Student { get; set; }
        public string? Notes { get; set; }
        public string? PaymentStatus { get; set; }

    }
}