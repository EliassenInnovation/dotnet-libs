using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Nucleus.Core.Shared.Persistence.Services.ServiceHelpers;
using Nucleus.Lesson.Contracts.Collections;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Contracts.Persistence;
using Nucleus.Lesson.Persistence.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Persistence.Services
{
    public class LessonsCalendarService : ILessonsCalendarService
    {
        private readonly ILessonMongoDatabase _db;
        private readonly ProjectionDefinition<LessonScheduleCollection, LessonsCalendarModel>? _lessonScheduleProjection;
        private readonly BsonCollectionBuilder<LessonsCalendarModel, LessonScheduleCollection> _lessonScheduleCollectionBuilder;
        private readonly BsonCollectionBuilder<LessonModel, LessonCollection> _lessonCollectionBuilder;

        public LessonsCalendarService(ILessonMongoDatabase db) 
        {
            _db = db;
            _lessonScheduleCollectionBuilder = new BsonCollectionBuilder<LessonsCalendarModel, LessonScheduleCollection>();
            _lessonCollectionBuilder = new BsonCollectionBuilder<LessonModel, LessonCollection>();
            _lessonScheduleProjection = Builders<LessonScheduleCollection>.Projection.Expression(item => new LessonsCalendarModel()
            {
                LessonScheduleId = item.LessonScheduleId,
                Title = item.Title,
                Tags = item.Tags,
                StartDateTime = item.StartDateTime,
                EndDateTime = item.EndDateTime,
                DurationEditable = true,
                AvailableToBook = true,
                Pro = item.Teacher.FullName,
                Repeat = item.Repeat
            });
        }

        public IQueryable<LessonsCalendarModel> Query() => _db.LessonSchedule.AsQueryable().Select(Projections.LessonsCalendar);

        public async Task<IQueryable<LessonsCalendarModel>> GetLessons(LessonsCalendarRequestModel calendarRequest)
        {
            DateTime startDate = new DateTime(calendarRequest.year, calendarRequest.month + 1, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            DateTimeOffset startDateTimeOffset = new DateTimeOffset(startDate);
            DateTimeOffset endDateTimeOffset = new DateTimeOffset(endDate);

            var query = from lessonSchedule in _db.LessonSchedule.AsQueryable()
                        join lesson in _db.Lessons.AsQueryable() on lessonSchedule.LessonScheduleId equals lesson.LessonScheduleId into lessons
                        from lesson in lessons.DefaultIfEmpty()
                        where (lessonSchedule.StartDateTime.Value >= startDateTimeOffset && lessonSchedule.StartDateTime.Value <= endDateTimeOffset)
                        || (lesson.LessonDateTime.Value >= startDateTimeOffset && lesson.LessonDateTime.Value <= endDateTimeOffset)
                        select new
                        {
                            LessonSchedule = lessonSchedule,
                            Lesson = lesson
                        };

            var result = query.Select(item => new LessonsCalendarModel
            {
                LessonScheduleId = item.LessonSchedule.LessonScheduleId,
                Title = item.LessonSchedule.Title,
                Tags = item.LessonSchedule.Tags,
                StartDateTime = item.Lesson.LessonDateTime != null ? item.Lesson.LessonDateTime : item.LessonSchedule.StartDateTime,
                EndDateTime = item.LessonSchedule.EndDateTime,
                DurationEditable = true,
                AvailableToBook = item.Lesson == null ? true : false,
                Pro = item.LessonSchedule.Teacher.FullName,
                Repeat = item.LessonSchedule.Repeat
            }).AsQueryable();

            return result;
        }
    }
}
