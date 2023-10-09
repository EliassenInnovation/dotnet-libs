using Eliassen.MongoDB.Extensions;
using MongoDB.Driver;
using Nucleus.Blog.Persistence.Collections;

namespace Nucleus.Blog.Persistence.Services
{
    public interface IBlogMongoDatabase
    {
        IMongoCollection<BlogCollection> Blogs { get; }
    }
}
