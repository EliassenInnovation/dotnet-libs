using Nucleus.Lesson.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Contracts.Persistence
{
    public interface ILessonsCalendarService
    {
        Task<IQueryable<LessonsCalendarModel>> GetLessons(LessonsCalendarRequestModel calendarRequest);

    }
}
