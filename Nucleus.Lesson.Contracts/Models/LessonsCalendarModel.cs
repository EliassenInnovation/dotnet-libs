using Eliassen.System.ComponentModel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Contracts.Models
{
    [SearchTermDefault(SearchTermDefaults.Contains)]
    public class LessonsCalendarModel
    {
        [NotSearchable]
        [IgnoreStringComparisonReplacement]
        public string? LessonScheduleId { get; set; }

        public string? Title { get; set; }

        public string[]? Tags { get; set; }  

        public DateTimeOffset? StartDateTime { get; set; }

        public DateTimeOffset? EndDateTime { get; set; }

        public Boolean DurationEditable { get; set; }

        public Boolean AvailableToBook { get; set; }

        public string? Repeat { get; set; }

        public string? Pro { get; set; }

        public double? Price { get; set; }

        public string? Student { get; set; }

        public int? Duration { get; set; }
    }
}
