﻿using Eliassen.MongoDB.Extensions;
using Eliassen.System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nucleus.Blog.Persistence.Services;
using Nucleus.Core.Persistence.Services;
using Nucleus.Lesson.Persistence.Services;
using Nucleus.Project.Persistence.Sevices;

namespace Nucleus.Dataloader.Cli
{
    internal class Program
    {
        static async Task Main(string[] args) =>
            await Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddCommandLine(args,
                        CommandLine.BuildParameters<DefaultMongoDatabaseSettings>()
                                   .AddParameters<DataLoaderSettings>()
                        );
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddConfiguration<DefaultMongoDatabaseSettings>(context.Configuration);
                    services.AddConfiguration<DataLoaderSettings>(context.Configuration);

                    services.AddHostedService<DataLoaderService>();

                    services
                        .AddMongoServices(context.Configuration)

                        .TryAddMongoDatabase<IBlogMongoDatabase>()
                        .TryAddMongoDatabase<ILessonMongoDatabase>()
                        .TryAddMongoDatabase<ICoreMongoDatabase>()
                        .TryAddMongoDatabase<IProjectMongoDatabase>()
                        ;
                })
                .RunConsoleAsync();

    }
}