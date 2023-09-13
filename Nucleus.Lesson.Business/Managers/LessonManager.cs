﻿using Nucleus.Core.Contracts.Models;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Models.Filters;
using Nucleus.Lesson.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Business.Managers
{
    public class LessonManager : ILessonManager
    {

        private readonly ILessonService _lessonService;

        public LessonManager(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<LessonModel?> GetLesson(string lessonId) =>
          await _lessonService.GetAsync(lessonId);

        public async Task<List<LessonModel>?> GetLessonsByLessonScheduleId(string scheduleId) =>

            await _lessonService.GetLessonsByLessonScheduleId(scheduleId);
        




#warning retire this
        public async Task<PagedResult<LessonModel>> GetLessonsPagedAsync(LessonsFilter lessonsFilter)
        {
            lessonsFilter.PagingModel ??= PagingModel.Default;
            List<LessonModel> blogs = await _lessonService.GetPagedAsync(lessonsFilter.PagingModel, lessonsFilter.LessonFilters, false);
            var result = new PagedResult<LessonModel>()
            {
                CurrentPage = lessonsFilter.PagingModel.CurrentPage,
                PageSize = lessonsFilter.PagingModel.PageSize,
                Results = blogs,
                RowCount = await _lessonService.GetPagedCountAsync(lessonsFilter.PagingModel, lessonsFilter.LessonFilters, false),
                PageCount = blogs.Count
            };
            return result;
        }

        public async Task<ResponseModel<LessonModel?>> SaveLessonAsync(LessonModel lesson)
        {
            if (lesson == null || lesson.LessonDateTime == null || string.IsNullOrEmpty(lesson.PaymentStatus))
                return new ResponseModel<LessonModel?>()
                {
                    IsSuccess = false,
                    Message = "Missing Required Fields"
                };
            ResponseModel<LessonModel?> result = new ResponseModel<LessonModel?>() { IsSuccess = true };
            if (String.IsNullOrEmpty(lesson.LessonId))
            {
                result.Response = await _lessonService.CreateAsync(lesson);
                return result;
            }
            else
            {
                await _lessonService.UpdateAsync(lesson);
                result.Response = lesson;
                return result;
            }
        }
    }
}
