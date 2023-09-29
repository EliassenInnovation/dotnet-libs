﻿using Eliassen.MongoDB.Extensions;
using System;

namespace Nucleus.Lesson.Persistence.Collections
{
    [CollectionName("lessons")]
    public class LessonCollection
    {
        public string? LessonId { get; set; }

        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? MediaLink { get; set; }
        public string? PreviewImage { get; set; }
        public string? Preview { get; set; }
        public string? Content { get; set; }
        public bool? Enabled { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string[]? Attendees { get; set; }
        public string? Teacher { get; set; }
        public int? Duration { get; set; }
        public string[]? Tags { get; set; }
        public double Price { get; set; }
        public string[]? Goals { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string? Notes { get; set; }
    }
}
