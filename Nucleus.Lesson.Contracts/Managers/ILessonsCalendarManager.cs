using Nucleus.Lesson.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Contracts.Managers
{
    public interface ILessonsCalendarManager
    {
        Task<IQueryable<LessonsCalendarModel>> GetLessonsByMonth(LessonsCalendarRequestModel calendarRequest);
    }
}
