using Nucleus.Lesson.Contracts.Models;
using Nucleus.Lesson.Persistence.Collections;
using System;
using System.Linq.Expressions;

namespace Nucleus.Lesson.Persistence.Services
{
    public static class Projections
    {
        public static Expression<Func<LessonCollection, LessonModel>> Lessons => item => new LessonModel()
        { 
            LessonId = item.LessonId,
            LessonScheduleId = item.LessonScheduleId,
            LessonDateTime = item.LessonDateTime,
            Student = item.Student,
            Notes = item.Notes,
            PaymentStatus = item.PaymentStatus
            
        };
        public static Expression<Func<Contracts.Collections.LessonScheduleCollection, LessonScheduleModel>> LessonSchedule => item => new LessonScheduleModel()
        {
            LessonScheduleId = item.LessonScheduleId,
            Content = item.Content,
            Preview = item.Preview,
            Slug = item.Slug,
            Title = item.Title,
            Enabled = item.Enabled,
            CreatedOn = item.CreatedOn,
            Teacher =new TeacherModel
            {
                EmailAddress = item.Teacher.EmailAddress,
                FullName = item.Teacher.FullName,
                UserId = item.Teacher.UserId,
            },
            Duration = item.Duration,
            Price = item.Price,
            Tags = item.Tags,
            StartDateTime = item.StartDateTime,
            EndDateTime = item.EndDateTime,
            Repeat = item.Repeat
        };
    }
}
