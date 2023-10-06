using Eliassen.System.ComponentModel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Contracts.Models
{
    [SearchTermDefault(SearchTermDefaults.Contains)]
    public class TeacherModel
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        
    }
}
