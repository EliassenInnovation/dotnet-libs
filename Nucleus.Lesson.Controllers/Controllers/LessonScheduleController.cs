using Eliassen.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Core.Contracts;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;

namespace Nucleus.Lesson.Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonScheduleController : ControllerBase
    {
        private readonly ILessonScheduleManager _LessonScheduleManager;
        private readonly IPublicLessonScheduleManager _publicLessonScheduleManager;

        public LessonScheduleController(ILessonScheduleManager LessonScheduleManager, 
            IPublicLessonScheduleManager publicLessonScheduleManager)
        {
            _LessonScheduleManager = LessonScheduleManager;
            _publicLessonScheduleManager = publicLessonScheduleManager;
        }

        [HttpGet("Lesson/{id}")]
        [ApplicationRight(Rights.Lesson.Manager)]
        public async Task<IActionResult> GetAsync(string id) =>
            new JsonResult(await _LessonScheduleManager.GetLesson(id));

        [HttpGet("Slug/{id}")]
        public async Task<IActionResult> GetLessonSlug(string id) =>
            new JsonResult(await _publicLessonScheduleManager.GetLessonSlug(id));

        [HttpPost("Query")]
        public IQueryable<LessonScheduleModel> ListLessons() => _publicLessonScheduleManager.QueryLessons();

        [HttpPost("Save")]
        //[ApplicationRight(Rights.Lesson.Manager)]
        public async Task<IActionResult> SaveAsync(LessonScheduleModel lesson) =>
            new JsonResult(await _LessonScheduleManager.SaveLessonAsync(lesson));

    }
}
