﻿using MongoDB.Bson.Serialization.Attributes;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Persistence.Collections;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nucleus.Lesson.Contracts.Collections
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
        public Boolean? Enabled { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public TeacherModel? Teacher { get; set; }
        public int? Duration { get; set; }
        public string[]? Tags { get; set; }
        public double Price { get; set; }
        public string[]? Goals { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string? Repeat { get; set; }
    }
}
