﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using FluentValidation;
using Gnome.Api.Bindings;
using Gnome.Api.Middleware;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.Mappings;
using Gnome.Application.Services;
using Gnome.Application.Validators;
using Gnome.Domain.Interfaces;
using Gnome.Infrastructure;
using Gnome.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
if (string.IsNullOrEmpty(cloudinaryUrl))
{
    throw new InvalidOperationException("Cloudinary URL not found in environment variables");
}

var cloudinary = new Cloudinary(cloudinaryUrl);
cloudinary.Api.Secure = true;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ListProductsQueryCommandHandler).Assembly));

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<AddCategoryCommandValidator>();

// Access configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Configuration/appsettings.json")
    .AddJsonFile($"Configuration/appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var connectionString = builder.Configuration.GetConnectionString("GnomeConnection");
Console.WriteLine($"Connection String: {connectionString}");

// If empty, check file paths
Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"File Exists: {File.Exists("appsettings.json")}");

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("GnomeConnection")));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IVariantRepository, VariantRepository>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton(new BinderConfiguration().CreateConfiguration());
builder.Services.AddSingleton(cloudinary);
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gnome API", Version = "v1" });
});

var app = builder.Build();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gnome API v1"));
}
else
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting Gnome API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}