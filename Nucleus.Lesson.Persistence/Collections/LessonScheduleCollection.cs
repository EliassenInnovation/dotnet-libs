using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Lesson.Persistence.Collections
{
    [BsonIgnoreExtraElements]
    public class LessonScheduleCollection
    {
        [Key]
        public string? LessonScheduleId { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? MediaLink { get; set; }
        public string? PreviewImage { get; set; }
        public string? Preview { get; set; }
        public string? Content { get; set; }
        public bool? Enabled { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public TeacherCollection? Teacher { get; set; }
        public int? Duration { get; set; }
        public string[]? Tags { get; set; }
        public double Price { get; set; }
        public string[]? Goals { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string? Repeat { get; set; }
    }
}
