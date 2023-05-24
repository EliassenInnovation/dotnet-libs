﻿using Microsoft.AspNetCore.Mvc;
using Nucleus.Blog.Contracts.Managers;

namespace Nucleus.Blog.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IPublicBlogManager _publicBlogManager { get; set; }

        public BlogController(IPublicBlogManager publicBlogManager)
        {
            _publicBlogManager = publicBlogManager;
        }

        //TODO: restore
#warning RESTORE THIS FEATURE
        //[HttpPost("Blogs")]
        //public async Task<IActionResult> GetAllBlogsPagedAsync(BlogsFilter filter) =>
        //    new JsonResult(await _publicBlogManager.GetBlogsPagedAsync(filter));

        [HttpGet("Slug/{id}")]
        public async Task<IActionResult> GetBlogSlug(string id) =>
            new JsonResult(await _publicBlogManager.GetBlogSlug(id));

        [HttpGet("RecentBlogs/{id}")]
        public async Task<IActionResult> GetRecentBlogs(int id) =>
            new JsonResult(await _publicBlogManager.GetRecentBlogs(id));

    }
}
