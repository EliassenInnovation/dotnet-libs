using MongoDB.Driver;
using Nucleus.Project.Persistence.Collections;

namespace Nucleus.Project.Persistence.Services
{
    public interface IProjectMongoDatabase
    {
        IMongoCollection<ProjectCollection> Projects { get; }
    }
}
