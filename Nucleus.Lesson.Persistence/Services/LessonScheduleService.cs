using MongoDB.Driver;
using Nucleus.Core.Shared.Persistence.Services.ServiceHelpers;
using Nucleus.Lesson.Contracts.Collections;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Models.Filters;
using Nucleus.Lesson.Contracts.Persistence;
using Nucleus.Lesson.Persistence.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Persistence.Services
{
    public class LessonScheduleService : ILessonScheduleService
    {
        private readonly ILessonMongoDatabase _db;
        private readonly ProjectionDefinition<LessonScheduleCollection, LessonScheduleModel>? _lessonProjection;
        private readonly BsonCollectionBuilder<LessonScheduleModel, LessonScheduleCollection> _lessonCollectionBuilder;

        public LessonScheduleService(
            ILessonMongoDatabase db
            )
        {
            _db = db;

            _lessonCollectionBuilder = new BsonCollectionBuilder<LessonScheduleModel, LessonScheduleCollection>();
            _lessonProjection = Builders<LessonScheduleCollection>.Projection.Expression(item => new LessonScheduleModel()
            {
                LessonScheduleId = item.LessonScheduleId,
                Title = item.Title,
                Slug = item.Slug,
                MediaLink = item.MediaLink,
                PreviewImage = item.PreviewImage,
                Preview = item.Preview,
                Content = item.Content,
                Enabled = item.Enabled,
                CreatedOn = item.CreatedOn,
                Teacher = new TeacherModel
                {
                    EmailAddress = item.Teacher.EmailAddress,
                    FullName = item.Teacher.FullName,
                    UserId = item.Teacher.UserId,
                },
                Duration = item.Duration,
                Tags = item.Tags,
                Price = item.Price,
                Goals = item.Goals,
                StartDateTime = item.StartDateTime,
                EndDateTime = item.EndDateTime,
                Repeat = item.Repeat
            });
        }

        public DateTimeOffset GetEndDateTime(DateTimeOffset startDateTime, int? duration)
        {
            return startDateTime.AddMinutes(Convert.ToDouble(duration));
        }

        // need to extend/re-work this so I do not pass in multiple parameters to each method, just a proper filter item from the business layer
        private FilterDefinition<LessonScheduleCollection> GetLessonsPredicateBuilder(bool onlyActive, LessonsFilterItem? filterItems)
        {
            // Keeping this business logic in the access layer.  Cannot move it to the business layer yet
            // until I can create an extension that can translate for multiple database.  Moving this to db
            // layer forces you to include mongo drivers which will no longer make this a good solution to be
            // a hybrid database solution just by changing interfaces in the IOC
            var builder = Builders<LessonScheduleCollection>.Filter;
            var filter = builder.Empty;

            //if (onlyActive)
            //    filter &= builder.AnyEq("enabled", true);

            if (filterItems != null && !string.IsNullOrWhiteSpace(filterItems.InputValue))
            {
                var textSearch = builder.Text(filterItems.InputValue);
                filter &= textSearch;
            }

            return filter;
        }

        public async Task<List<LessonScheduleModel>> GetAsync() =>
            await _db.LessonSchedule.Find(_ => true).Project(_lessonProjection).ToListAsync();

        public async Task<List<LessonScheduleModel>> GetRecentAsync(int i, bool onlyActive) =>
           await _db.LessonSchedule.Find(_ => true)
               .Sort("{ \"createdOn\": -1}")
               .Limit(i)
               .Project(_lessonProjection).ToListAsync();

        public async Task<LessonScheduleModel?> GetSlugAsync(string slug, bool onlyActive) =>
            await _db.LessonSchedule.Find(x => x.Slug == slug).Project(_lessonProjection).FirstOrDefaultAsync();

        public async Task<LessonScheduleModel?> GetAsync(string id) =>
            await _db.LessonSchedule.Find(x => x.LessonScheduleId == id).Project(_lessonProjection).FirstOrDefaultAsync();

        public async Task<LessonScheduleModel> CreateAsync(LessonScheduleModel newLesson)
        {
            LessonScheduleCollection lesson = _lessonCollectionBuilder.BuildCollection(newLesson);
            await _db.LessonSchedule.InsertOneAsync(lesson);
            newLesson.LessonScheduleId = lesson.LessonScheduleId;
            return newLesson;
        }

        public async Task UpdateAsync(LessonScheduleModel updatedLesson) =>
            await _db.LessonSchedule.ReplaceOneAsync(x => x.LessonScheduleId == updatedLesson.LessonScheduleId, _lessonCollectionBuilder.BuildCollection(updatedLesson));

        public async Task RemoveAsync(string id) =>
            await _db.LessonSchedule.DeleteOneAsync(x => x.LessonScheduleId == id);

        public IQueryable<LessonScheduleModel> Query()
        {
            var query = from lessonSchedule in _db.LessonSchedule.AsQueryable()
                        join lesson in _db.Lessons.AsQueryable() on lessonSchedule.LessonScheduleId equals lesson.LessonScheduleId into lessons
                        select new
                        {
                            LessonSchedule = lessonSchedule,
                            Lessons = lessons
                        };

            var result = query.Select(item => new LessonScheduleModel
            {
                LessonScheduleId = item.LessonSchedule.LessonScheduleId,
                Title = item.LessonSchedule.Title,
                Slug = item.LessonSchedule.Slug,
                MediaLink = item.LessonSchedule.MediaLink,
                PreviewImage = item.LessonSchedule.PreviewImage,
                Preview = item.LessonSchedule.Preview,
                Content = item.LessonSchedule.Content,
                Enabled = item.LessonSchedule.Enabled,
                CreatedOn = item.LessonSchedule.CreatedOn,
                Teacher = new TeacherModel
                {
                    UserId = item.LessonSchedule.Teacher.UserId,
                    FullName = item.LessonSchedule.Teacher.FullName,
                    EmailAddress = item.LessonSchedule.Teacher.EmailAddress,
                },
                Duration = item.LessonSchedule.Duration,
                Tags = item.LessonSchedule.Tags,
                Price = item.LessonSchedule.Price,
                Goals = item.LessonSchedule.Goals,
                StartDateTime = item.LessonSchedule.StartDateTime,
                EndDateTime = item.LessonSchedule.EndDateTime,
                Repeat = item.LessonSchedule.Repeat,
                NumberOfLessons = item.Lessons.Count()
            });

            return result;

        }


    }
}
