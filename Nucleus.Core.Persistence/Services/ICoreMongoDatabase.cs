using Eliassen.MongoDB.Extensions;
using MongoDB.Driver;
using Nucleus.Core.Persistence.Collections;

namespace Nucleus.Core.Persistence.Services
{
    public interface ICoreMongoDatabase
    {
        IMongoCollection<UserCollection> Users { get; }
        IMongoCollection<DocumentCollection> Documents { get; }
        IMongoCollection<ModuleCollection> Modules { get; }
    }
}
