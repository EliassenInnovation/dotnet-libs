using Eliassen.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Blog.Contracts.Managers;
using Nucleus.Blog.Contracts.Models;
using Nucleus.Blog.Contracts.Models.Filters;
using Nucleus.Core.Contracts;

namespace Nucleus.Blog.Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdminController : ControllerBase
    {
        private readonly IBlogManager _blogManager;

        public BlogAdminController(IBlogManager blogManager)
        {
            _blogManager = blogManager;
        }

#warning retire this
        [HttpPost("Blogs")]
        [ApplicationRight(Rights.Blog.Manager)]
        [Obsolete]
        public async Task<IActionResult> GetAllBlogsPagedAsync(BlogsFilter filter) =>
            new JsonResult(await _blogManager.GetBlogsPagedAsync(filter));

        [HttpGet("Blog/{id}")]
        [ApplicationRight(Rights.Blog.Manager)]
        public async Task<IActionResult> GetAsync(string id) =>
            new JsonResult(await _blogManager.GetBlog(id));

        [HttpPost("Save")]
        [ApplicationRight(Rights.Blog.Manager)]
        public async Task<IActionResult> SaveAsync(BlogModel blog) =>
            new JsonResult(await _blogManager.SaveBlogAsync(blog));


        [HttpPost("Query")]
        [ApplicationRight(Rights.Blog.Manager)]
        public IQueryable<BlogModel> ListLessons() => _blogManager.QueryBlogs();

    }
}
