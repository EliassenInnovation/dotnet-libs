using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Nucleus.Lesson.Business.Managers
{
    public class LessonsCalendarManager : ILessonsCalendarManager
    {
        private readonly ILessonsCalendarService _lessonsCalendarService;
        public LessonsCalendarManager(ILessonsCalendarService lessonsCalendarService)
        {
            _lessonsCalendarService = lessonsCalendarService;
        }

        public IReadOnlyCollection<LessonsCalendarModel> GetLessonsByMonth(LessonsCalendarRequestModel calendarRequest)
        {
           return _lessonsCalendarService.GetLessons(calendarRequest).ToArray();
        }
    }
}
