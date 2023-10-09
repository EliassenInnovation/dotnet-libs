using Eliassen.System.Accessors;
using Eliassen.System.Configuration;
using Eliassen.System.Linq.Expressions;
using Eliassen.System.Linq.Search;
using Eliassen.System.ResponseModel;
using Eliassen.System.Security.Cryptography;
using Eliassen.System.Text.Json.Serialization;
using Eliassen.System.Text.Templating;
using Eliassen.System.Text.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Dataloader.Cli;
using System.ComponentModel;
using System.Linq;

namespace Eliassen.System;

/// <summary>
/// Suggested IOC configurations
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This will add all available extensions to the IOC container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection TryAllSystemExtensions(this IServiceCollection services, IConfiguration config) =>
        services
        .TryAddSearchQueryExtensions()
        .TrySecurityExtensions()
        .TryTemplatingExtensions(config)
        .TrySerializerExtensions()
        ;

    /// <summary>
    /// Add support for shared SearchQuery Extensions
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection TryAddSearchQueryExtensions(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(IQueryBuilder<>), typeof(QueryBuilder<>));
        services.TryAddTransient(typeof(ISortBuilder<>), typeof(SortBuilder<>));
        services.TryAddTransient(typeof(IExpressionTreeBuilder<>), typeof(ExpressionTreeBuilder<>));

        services.AddTransient<IPostBuildExpressionVisitor, StringComparisonReplacementExpressionVisitor>();

        //services.AddTransient<IPostBuildExpressionVisitor, SkipInstanceMethodOnNullExpressionVisitor>();

        services.TryAddScoped<ICaptureResultMessage, CaptureResultMessage>();
        return services;
    }

    /// <summary>
    /// Add support for shared security extensions
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection TrySecurityExtensions(this IServiceCollection services)
    {
        services.TryAddTransient<IHash, Hash>();
        return services;
    }

    /// <summary>
    /// Add support for shared Serializer
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection TrySerializerExtensions(this IServiceCollection services)
    {
        services.TryAddSingleton<ISerializer>(sp => sp.GetRequiredService<IJsonSerializer>());
        services.TryAddSingleton<IJsonSerializer, DefaultJsonSerializer>();
        services.TryAddSingleton<IBsonSerializer, DefaultBsonSerializer>();
        //TODO: services.TryAddSingleton<IXmlSerializer, DefaultXmlSerializer>();
        services.TryAddSingleton(_ => DefaultJsonSerializer.DefaultOptions);
        return services;
    }

    /// <summary>
    /// Add support for shared Templating
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection TryTemplatingExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.TryAddTransient<ITemplateEngine, TemplateEngine>();

        services.AddTransient<ITemplateSource, FileTemplateSource>();
        services.AddConfiguration<FileTemplatingSettings>(config);

        services.AddTransient<ITemplateProvider, XsltTemplateProvider>();

        services.AddTransient<IFileType>(_ => new FileType { Extension = ".md", ContentType = "text/markdown", IsTemplateType = false });
        services.AddTransient<IFileType>(_ => new FileType { Extension = ".yaml", ContentType = "text/yaml", IsTemplateType = false });

        services.AddTransient<IFileType>(_ => new FileType { Extension = ".html", ContentType = "text/html", IsTemplateType = false });
        services.AddTransient<IFileType>(_ => new FileType { Extension = ".txt", ContentType = "text/plain", IsTemplateType = false });
        services.AddTransient<IFileType>(_ => new FileType { Extension = ".json", ContentType = DefaultJsonSerializer.DefaultContentType, IsTemplateType = false });
        services.AddTransient<IFileType>(_ => new FileType { Extension = ".xml", ContentType = DefaultXmlSerializer.DefaultContentType, IsTemplateType = false });

        services.AddTransient<IFileType>(_ => new FileType { Extension = ".xslt", ContentType = XsltTemplateProvider.ContentType, IsTemplateType = true });

        return services;
    }

    /// <summary>
    /// Register accessor type that is scoped to as AsyncLocal
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAccessor<TService>(this IServiceCollection services)
        where TService : class
    {
        services.TryAddSingleton(typeof(IAccessor<>), typeof(Accessor<>));
        return services;
    }

    /// <summary>
    /// Extend configuration options
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguration<TConfig>(
        this IServiceCollection services,
        IConfiguration config
        )
        where TConfig : class
    {
        var section = TypeDescriptor.GetAttributes(typeof(TConfig))
            .OfType<ConfigurationSectionAttribute>()
            .FirstOrDefault()?.ConfigurationSection ?? typeof(TConfig).Name;
        services.Configure<TConfig>(config.GetSection(section));
        return services;
    }

    /// <summary>
    /// Get singleton instance from IOC container
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="services"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static IServiceCollection GetSingletonInstance<TService, TInstance>(this IServiceCollection services, out TInstance instance)
        where TService : class
        where TInstance : class, TService, new()
    {
        instance = (services.FirstOrDefault(i => i.ServiceType == typeof(TService))?.ImplementationInstance as TInstance) ?? new TInstance();
        services.Replace(ServiceDescriptor.Singleton<TService>(instance));
        return services;
    }
}
