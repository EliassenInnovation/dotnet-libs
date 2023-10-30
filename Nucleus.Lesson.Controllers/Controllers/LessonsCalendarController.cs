using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonsCalendarController : ControllerBase
    {
        private readonly ILessonsCalendarManager _lessonsCalendarManager;

        public LessonsCalendarController(ILessonsCalendarManager lessonsCalendarManager)
        {
            _lessonsCalendarManager = lessonsCalendarManager;
        }

        [HttpPost]
        public Task<IQueryable<LessonsCalendarModel>> GetLessonsByMonth(LessonsCalendarRequestModel calendarRequest) => 
            _lessonsCalendarManager.GetLessonsByMonth(calendarRequest);
    }
}
