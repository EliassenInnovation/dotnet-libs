﻿using Eliassen.MailKit.Hosting;
using Eliassen.MessageQueueing.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eliassen.Common.Hosting;

/// <summary>
/// Provides extension methods for configuring common hosting services in the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Tries to add common hosting services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration">The configuration containing settings for hosting services.</param>
    /// <param name="builder">Optional builder for configuring hosting. Default is <c>null</c>.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection TryCommonHosting(
        this IServiceCollection services,
        IConfiguration configuration,
#if DEBUG
        HostingBuilder? builder
#else
        HostingBuilder? builder = default
#endif
    )
    {
        builder ??= new();

        if (!builder.DisableMailKit)
            services.TryAddMailKitHosting();

        if (!builder.DisableMessageQueueing)
            services.TryAddMessageQueueingHosting();

        return services;
    }
}
