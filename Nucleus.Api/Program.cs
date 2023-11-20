using Eliassen.AspNetCore.Mvc;
using Eliassen.MongoDB.Extensions;
using Eliassen.System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nucleus.AspNetCore.Mvc;
using Nucleus.Core.Business;
using Nucleus.Core.Controllers;
using Nucleus.Core.Persistence;
using Nucleus.Core.Shared.Business;
using Nucleus.External.Azure.StorageAccount;
using Nucleus.External.Microsoft.B2C;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Shared Modules
builder.Services
    .AddMongoServices(builder.Configuration)
    .TryAllSystemExtensions(builder.Configuration)
    .AddMicrosoftB2CServices()
    .AddAzureStorageAccountServices()
    .AddAspNetCoreExtensions()
    .AddApplicationAspNetCoreServices()
    ;

// Add additional assemblies here so we can keep our API Project clean and easily scalable
builder.Services.AddControllers()
    .AddApplicationPart(Assembly.Load("Nucleus.Core.Controllers"))
    .AddApplicationPart(Assembly.Load("Nucleus.Core.Shared.Controllers"))
    ;

// Adding Module Registrations for IOC
builder.Services
    .AddCorePersistenceServices()
    .AddCoreBusinessServices()
    .AddCoreWebServices()

    .AddSharedBusinessServices()
    ;


// B2C Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Add the primary JWT bearer configuration for an Azure AD B2C policy.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Azure:AdB2C:Instance"] + builder.Configuration["Azure:AdB2C:Domain"] + "/" + builder.Configuration["Azure:AdB2C:Policy"] + "/v2.0/";
        options.Audience = builder.Configuration["Azure:AdB2C:ClientId"];
    });

builder.Services.Configure<AzureB2CConfig>(options => builder.Configuration.Bind(AzureB2CConfig.ConfigKey, options));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    policy =>
    {
        policy
            .WithOrigins(builder.Configuration["CORS:AllowedOrigins"]?.Split(';') ?? Array.Empty<string>())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            ;
    });
});

builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAspNetCoreExtensionMiddleware();

app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
