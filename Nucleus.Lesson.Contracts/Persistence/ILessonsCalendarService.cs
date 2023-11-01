using Nucleus.Lesson.Contracts.Models;
using System.Linq;

namespace Nucleus.Lesson.Contracts.Persistence
{
    public interface ILessonsCalendarService
    {
        IQueryable<LessonsCalendarModel> GetLessons(LessonsCalendarRequestModel calendarRequest);
    }
}
