using Eliassen.MongoDB.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Lesson.Contracts.Persistence;
using Nucleus.Lesson.Persistence.Services;

namespace Nucleus.Lesson.Persistence
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddLessonPersistenceServices(this IServiceCollection services)
        {
            services.TryAddMongoDatabase<ILessonMongoDatabase>();

            services.TryAddTransient<ILessonScheduleService, LessonScheduleService>();
            services.TryAddTransient<ILessonService, LessonService>();
            services.TryAddTransient<ILessonsCalendarService, LessonsCalendarService>();
            return services;
        }
    }
}
