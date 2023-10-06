using Nucleus.Core.Contracts.Models;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Persistence;
using System;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Business.Managers
{
    public class LessonScheduleManager : ILessonScheduleManager
    {

        private readonly ILessonScheduleService _LessonScheduleService;

        public LessonScheduleManager(ILessonScheduleService LessonScheduleService)
        {
            _LessonScheduleService = LessonScheduleService;
        }

        public async Task<LessonScheduleModel?> GetLesson(string lessonId) =>
          await _LessonScheduleService.GetAsync(lessonId);

        public async Task<ResponseModel<LessonScheduleModel?>> SaveLessonAsync(LessonScheduleModel lesson)
        {
            if (lesson == null)
                return new ResponseModel<LessonScheduleModel?>()
                {
                    IsSuccess = false,
                    Message = "Missing Required Fields. Lesson cannot be null."
                };
            ResponseModel<LessonScheduleModel?> result = new ResponseModel<LessonScheduleModel?>() { IsSuccess = true };
            if (String.IsNullOrEmpty(lesson.LessonScheduleId))
            {
                result.Response = await _LessonScheduleService.CreateAsync(lesson);
                return result;
            }
            else
            {
                await _LessonScheduleService.UpdateAsync(lesson);
                result.Response = lesson;
                return result;
            }
        }
    }
}
