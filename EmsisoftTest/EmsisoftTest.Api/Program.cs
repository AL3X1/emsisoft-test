using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using EmsisoftTest.Data.Contexts;
using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Infrastructure.Initializers;
using EmsisoftTest.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace EmsisoftTest.Api;

// TODO: Convert to .NET 6 app
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        var settings = new AppSettings();
        builder.Configuration.Bind(nameof(AppSettings), settings);
        services.AddSingleton(settings);

        services.AddDbContext<ApplicationDbContext>(x =>
        {
            x.EnableSensitiveDataLogging(false);
            x.UseSqlServer(settings.Database.ConnectionString);
        }, ServiceLifetime.Transient);

        services.AddMediatR(x =>
        {
            var assemblies = new[]
            {
                typeof(Program).Assembly,
            };
            
            x.RegisterServicesFromAssemblies(assemblies);
        });
        
        services.AddControllers();
        ConfigureSwagger(services);
        
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(ContainerInitializer.Initialize));
        MessageQueueInitializer.Initialize(settings.Queue);
        var app = builder.Build();

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        UseSwagger(app);
        
        await app.RunAsync();
    }

    private static void UseSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = "docs";
        });
    }

    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Web API",
                Version = "v1",
            });

            options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "API Access token",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" }
                    },
                    new string[] { }
                }
            });

            var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }
}