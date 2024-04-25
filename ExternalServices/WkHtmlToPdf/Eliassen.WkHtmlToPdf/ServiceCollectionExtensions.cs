﻿using Eliassen.Documents.Conversion;
using Eliassen.Documents.Models;
using Microsoft.Extensions.DependencyInjection;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace Eliassen.WkHtmlToPdf;

/// <summary>
/// Provides extension methods for configuring services related to WkHtmlToPdf.
/// </summary>
public static class ServiceCollectionExtensions
{
    private static IConverter? _converter;

    /// <summary>
    /// Configures services for WkHtmlToPdf.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection TryAddWkHtmlToPdfServices(this IServiceCollection services)
    {
        services.AddTransient<IDocumentConversionHandler, HtmlToPdfConversionHandler>();
        services.AddTransient<ITools, PdfTools>();
        services.AddSingleton<IConverter>(sp => _converter ??= ActivatorUtilities.CreateInstance<SynchronizedConverter>(sp));

        services.AddTransient<IDocumentType>(_ => new DocumentType
        {
            Name = "Portable Document Format",
            FileHeader = [(byte)'%', (byte)'P', (byte)'D', (byte)'F', (byte)'-'],
            FileExtensions = [".pdf",],
            ContentTypes = ["application/pdf"],
        });

        return services;
    }
}

