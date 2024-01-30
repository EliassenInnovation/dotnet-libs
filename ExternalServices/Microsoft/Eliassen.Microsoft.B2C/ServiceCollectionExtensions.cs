﻿using Eliassen.Identity.Identity;
using Eliassen.Microsoft.B2C.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Eliassen.Microsoft.B2C;

/// <summary>
/// Extension methods for adding Microsoft B2C services to the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Microsoft B2C services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which Microsoft B2C services should be added.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection TryAddMicrosoftB2CServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string microsoftIdentityConfigurationSection = nameof(MicrosoftIdentityOptions)
        )
    {
        services.TryAddTransient<IIdentityManager, ManageGraphUser>();
        services.TryAddTransient<IManageGraphUser, ManageGraphUser>();

        services.Configure<MicrosoftIdentityOptions>(options => configuration.Bind(microsoftIdentityConfigurationSection, options));

        return services;
    }
}
