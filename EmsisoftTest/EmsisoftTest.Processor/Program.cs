using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Data.Contexts;
using EmsisoftTest.Messaging;
using EmsisoftTest.Messaging.Interfaces;
using EmsisoftTest.Processor;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>(); 

        var settings = new AppSettings();
        context.Configuration.Bind(nameof(AppSettings), settings);
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

        services.AddSingleton<IMessageConsumer, MessageConsumer>();
    })
    .Build();

await builder.RunAsync();