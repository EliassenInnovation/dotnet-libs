using Eliassen.MongoDB.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Core.Contracts.Persistence;
using Nucleus.Core.Persistence.Services;

namespace Nucleus.Core.Persistence
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddCorePersistenceServices(this IServiceCollection services)
        {
            services.TryAddMongoDatabase<ICoreMongoDatabase>();

            services.TryAddTransient<IDocumentService, DocumentService>();
            services.TryAddTransient<IUserService, UserService>();
            services.TryAddTransient<IModuleService, ModuleServices>();
            services.TryAddTransient<IProfileService, ProfileService>();
            return services;
        }
    }
}
