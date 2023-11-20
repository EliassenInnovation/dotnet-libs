using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Core.Business.Managers;
using Nucleus.Core.Contracts.Managers;

namespace Nucleus.Core.Business
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddCoreBusinessServices(this IServiceCollection services)
        {
            services.TryAddTransient<IDocumentManager, DocumentManager>();
            services.TryAddTransient<IUserProfileManager, UserProfileManager>();
            services.TryAddTransient<IUserManagementManager, UserManagementManager>();
            services.TryAddTransient<IProfileManagementManager, ProfileManagementManager>();
            return services;
        }
    }
}
