using Nucleus.Blog.Contracts.Managers;
using Nucleus.Blog.Contracts.Models;
using Nucleus.Blog.Contracts.Models.Filters;
using Nucleus.Blog.Contracts.Persistence;
using Nucleus.Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Nucleus.Blog.Business.Managers
{
    public class BlogManager : IBlogManager
    {

        private readonly IBlogService _blogService;

        public BlogManager(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IQueryable<BlogModel> QueryBlogs() => _blogService.Query();

        public async Task<BlogModel?> GetBlog(string blogId) =>
            await _blogService.GetAsync(blogId, false);

#warning retire this
        public async Task<PagedResult<BlogModel>> GetBlogsPagedAsync(BlogsFilter blogsFilter)
        {
            blogsFilter.PagingModel ??= PagingModel.Default;
            var blogs = await _blogService.GetPagedAsync(blogsFilter.PagingModel, blogsFilter.BlogFilters, false);
            var result = new PagedResult<BlogModel>()
            {
                CurrentPage = blogsFilter.PagingModel.CurrentPage,
                PageSize = blogsFilter.PagingModel.PageSize,
                Results = blogs,
                RowCount = await _blogService.GetPagedCountAsync(blogsFilter.PagingModel, blogsFilter.BlogFilters, false),
                PageCount = blogs.Count
            };
            return result;
        }

        public async Task<ResponseModel<BlogModel?>> SaveBlogAsync(BlogModel blog)
        {
            if (blog == null || string.IsNullOrEmpty(blog.Title) || string.IsNullOrEmpty(blog.Content) || string.IsNullOrEmpty(blog.Slug) || string.IsNullOrEmpty(blog.Preview))
                return new ResponseModel<BlogModel?>()
                {
                    IsSuccess = false,
                    Message = "Missing Required Fields"
                };
            var result = new ResponseModel<BlogModel?>() { IsSuccess = true };
            if (string.IsNullOrEmpty(blog.BlogId))
            {
                blog.CreatedOn = DateTimeOffset.Now;
                result.Response = await _blogService.CreateAsync(blog);
                return result;
            }
            else
            {
                await _blogService.UpdateAsync(blog);
                result.Response = blog;
                return result;
            }
        }

    }
}
