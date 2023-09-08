﻿using Eliassen.System.ComponentModel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Contracts.Models
{
    [SearchTermDefault(SearchTermDefaults.Contains)]
    public class LessonModel
    {
        [NotSearchable]
        [IgnoreStringComparisonReplacement]
        public string? LessonId { get; set; }

        public string? LessonAdminId { get; set; }

        [DefaultSort(priority: 0)]
        public DateTimeOffset? LessonDateTime { get; set; }
       
        public string? Student { get; set; }
        public string? Notes { get; set; }
        public string? PaymentStatus { get; set; }


    }
}
