﻿using Eliassen.Extensions;
using Eliassen.Extensions.Configuration;
using Eliassen.Handlebars.Extensions;
using Eliassen.System;
using Eliassen.System.Text.Templating;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eliassen.TemplateEngine.Cli;

public class Program
{
    private static async Task Main(string[] args) =>
        await Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) => config.AddCommandLine(args,
                    CommandLine.BuildParameters<TemplateEngineOptions>()
                               .AddParameters<FileTemplatingOptions>()
                    ))
            .ConfigureServices((context, services) =>
            {
                services.Configure<TemplateEngineOptions>(options => context.Configuration.Bind(nameof(TemplateEngineOptions), options));

                services.AddHostedService<TemplateEngineService>();

                services.TryAddSystemExtensions(context.Configuration);
                services.TryAddHandlebarServices();
            })
            .StartAsync();
}
