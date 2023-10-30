using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Business.Managers
{
    public class LessonsCalendarManager : ILessonsCalendarManager
    {
        private readonly ILessonsCalendarService _lessonsCalendarService;
        public LessonsCalendarManager(ILessonsCalendarService lessonsCalendarService)
        {
            _lessonsCalendarService = lessonsCalendarService;
        }

        public async Task<IQueryable<LessonsCalendarModel>> GetLessonsByMonth(LessonsCalendarRequestModel calendarRequest) =>
            await _lessonsCalendarService.GetLessons(calendarRequest);
    }
}
