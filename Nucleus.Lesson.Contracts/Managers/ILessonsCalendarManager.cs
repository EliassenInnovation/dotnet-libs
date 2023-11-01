using Nucleus.Lesson.Contracts.Models;
using System.Collections.Generic;

namespace Nucleus.Lesson.Contracts.Managers
{
    public interface ILessonsCalendarManager
    {
        IReadOnlyCollection<LessonsCalendarModel> GetLessonsByMonth(LessonsCalendarRequestModel calendarRequest);
    }
}
