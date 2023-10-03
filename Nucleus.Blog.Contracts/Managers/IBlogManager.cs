using Nucleus.Blog.Contracts.Models;
using Nucleus.Blog.Contracts.Models.Filters;
using Nucleus.Core.Contracts.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.Blog.Contracts.Managers
{
    public interface IBlogManager
    {
        Task<BlogModel?> GetBlog(string blogId);
        Task<ResponseModel<BlogModel?>> SaveBlogAsync(BlogModel blog);

        IQueryable<BlogModel> QueryBlogs();

    }
}
