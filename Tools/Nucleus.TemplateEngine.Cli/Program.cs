﻿using Eliassen.System;
using Eliassen.System.Configuration;
using Eliassen.System.Templating;
using Eliassen.Handlebars.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Nucleus.TemplateEngine.Cli;
public class Program
{
    static async Task Main(string[] args) =>
        await Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddCommandLine(args,
                    CommandLine.BuildParameters<TemplateEngineSettings>()
                               .AddParameters<FileTemplatingSettings>()
                    );
            })
            .ConfigureServices((context, services) =>
            {
                services.AddConfiguration<TemplateEngineSettings>(context.Configuration);

                services.AddHostedService<TemplateEngineService>();

                services.TryAllSystemExtensions(context.Configuration);
                services.AddHandlebarServices();
            })
            .RunConsoleAsync();
}