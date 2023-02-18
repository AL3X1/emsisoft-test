using Autofac.Extensions.DependencyInjection;
using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Data.Contexts;
using EmsisoftTest.Infrastructure.Initializers;
using EmsisoftTest.Processor;
using EmsisoftTest.Processor.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(ContainerInitializer.Initialize))
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<HashProcessor>(); 

        var settings = new AppSettings();
        context.Configuration.Bind(nameof(AppSettings), settings);
        services.AddSingleton(settings);

        services.AddDbContext<ApplicationDbContext>(x =>
        {
            x.EnableSensitiveDataLogging(false);
            x.UseSqlServer(settings.Database.ConnectionString);
        }, ServiceLifetime.Transient);
    })
    .Build();

await builder.ApplyMigrationsAsync();
await builder.RunAsync();