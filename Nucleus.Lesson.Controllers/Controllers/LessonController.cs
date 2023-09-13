﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Models.Filters;

namespace Nucleus.Lesson.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LessonController : ControllerBase
    {
        private readonly IPublicLessonManager _publicLessonManager;

        public LessonController(IPublicLessonManager publicLessonManager)
        {
            _publicLessonManager = publicLessonManager;
        }
#warning retire this
        [Obsolete]

        [HttpPost("Lessons")]
        public async Task<IActionResult> GetAllLessons(LessonsFilter filter) =>
            new JsonResult(await _publicLessonManager.GetLessonsPagedAsync(filter));

        [HttpGet("Lessons/{id}")]
        public async Task<IActionResult> GetRecentLessons(int id) =>
            new JsonResult(await _publicLessonManager.GetRecentLessons(id));

        [HttpGet("Lessons/getByLessonScheduleId/{id}")]
        public async Task<IActionResult> GetLessonsByLessonScheduleId(string id) =>
            new JsonResult(await _publicLessonManager.GetLessonsByLessonScheduleId(id));

        [HttpPost("Query")]
        public IQueryable<LessonModel> ListLessons() => _publicLessonManager.QueryLessons();

    }
}
