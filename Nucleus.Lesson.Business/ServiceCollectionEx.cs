using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Lesson.Business.Managers;
using Nucleus.Lesson.Contracts.Managers;

namespace Nucleus.Lesson.Business
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddLessonBusinessServices(this IServiceCollection services)
        {
            services.TryAddTransient<ILessonManager, LessonManager>();
            services.TryAddTransient<ILessonScheduleManager, LessonScheduleManager>();
            services.TryAddTransient<IPublicLessonManager, PublicLessonManager>();
            services.TryAddTransient<IPublicLessonScheduleManager, PublicLessonScheduleManager>();
            services.TryAddTransient<ILessonsCalendarManager, LessonsCalendarManager>();
            return services;
        }
    }
}
