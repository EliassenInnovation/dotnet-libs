using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;

namespace Nucleus.Lesson.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LessonsCalendarController : ControllerBase
    {
        private readonly ILessonsCalendarManager _lessonsCalendarManager;

        public LessonsCalendarController(ILessonsCalendarManager lessonsCalendarManager)
        {
            _lessonsCalendarManager = lessonsCalendarManager;
        }

        [HttpPost]
        public IReadOnlyCollection<LessonsCalendarModel> GetLessonsByMonth(LessonsCalendarRequestModel calendarRequest) => 
            _lessonsCalendarManager.GetLessonsByMonth(calendarRequest);
    }
}
