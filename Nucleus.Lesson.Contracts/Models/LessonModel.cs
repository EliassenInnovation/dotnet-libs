using Eliassen.System.ComponentModel.Search;
using System;

namespace Nucleus.Lesson.Contracts.Models
{
    [SearchTermDefault(SearchTermDefaults.Contains)]
    public class LessonModel
    {
        [NotSearchable]
        [IgnoreStringComparisonReplacement]
        public string? LessonId { get; set; }

        public string? LessonScheduleId { get; set; }

        [DefaultSort(priority: 0)]
        public DateTimeOffset? LessonDateTime { get; set; }
       
        public string? Student { get; set; }
        public string? Notes { get; set; }
        public string? PaymentStatus { get; set; }

    }
}
