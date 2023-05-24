﻿using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Business.Managers
{
    public class PublicLessonManager : IPublicLessonManager
    {

        private readonly ILessonService _lessonService;

        public PublicLessonManager(ILessonService LessonService)
        {
            _lessonService = LessonService;
        }
        //TODO: restore
#warning RESTORE THIS FEATURE
        //public async Task<PagedResult<LessonModel>> GetLessonsPagedAsync(LessonsFilter lessonsFilter)
        //{
        //    List<LessonModel> lessons = await _lessonService.GetPagedAsync(lessonsFilter.PagingModel, lessonsFilter.LessonFilters, true);
        //    PagedResult<LessonModel> result = new PagedResult<LessonModel>()
        //    {
        //        CurrentPage = lessonsFilter.PagingModel.CurrentPage,
        //        PageSize = lessonsFilter.PagingModel.PageSize,
        //        Results = lessons,
        //        RowCount = await _lessonService.GetPagedCountAsync(lessonsFilter.PagingModel, lessonsFilter.LessonFilters, true),
        //        PageCount = lessons.Count
        //    };
        //    return result;
        //}

        public async Task<List<LessonModel>> GetLessons() =>
          await _lessonService.GetAsync();

        public async Task<LessonModel?> GetLessonSlug(string slug) =>
          await _lessonService.GetSlugAsync(slug, true);

        public async Task<List<LessonModel>?> GetRecentLessons(int i) =>
            await _lessonService.GetRecentAsync(i, true);
    }
}
