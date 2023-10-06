using Nucleus.Core.Contracts.Models;
using Nucleus.Lesson.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.Lesson.Contracts.Persistence
{
    public interface ILessonService
    {

        Task<List<LessonModel>> GetAsync();

        Task<List<LessonModel>> GetRecentAsync(int i, bool onlyActive);

        Task<LessonModel?> GetAsync(string id);

        Task<LessonModel> CreateAsync(LessonModel newBlog);

        Task UpdateAsync(LessonModel updatedBlog);

        Task RemoveAsync(string id);
        IQueryable<LessonModel> Query();
        void UpdateLesson(LessonModel lesson);
        void UpdateLessons(LessonModel[] lessons);
    }
}
