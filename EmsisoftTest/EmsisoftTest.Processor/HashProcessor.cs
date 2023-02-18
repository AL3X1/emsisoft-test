using EmsisoftTest.Domain.Commands;
using EmsisoftTest.Infrastructure.Interfaces;
using EmsisoftTest.Messaging;
using EmsisoftTest.Messaging.Interfaces;

namespace EmsisoftTest.Processor;

public class HashProcessor : BackgroundService
{
    private const int MaxConcurrencyDegree = 4;
    
    private readonly ILogger<HashProcessor> _logger;
    private readonly IMessageConsumer _messageConsumer;
    private readonly IServiceProvider _serviceProvider;

    public HashProcessor(ILogger<HashProcessor> logger, IMessageConsumer messageConsumer, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _messageConsumer = messageConsumer;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = MaxConcurrencyDegree
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var messages = _messageConsumer.FetchMessages<string>();
                await Parallel.ForEachAsync(messages, parallelOptions, ProcessMessage);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.ToString());
            }
        }
    }

    private async ValueTask ProcessMessage(MessagePayload<string> message, CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<HashProcessor>>();
        var commandBuilder = scope.ServiceProvider.GetRequiredService<ICommandBuilder>();
        
        logger.LogInformation($"Processing {message.Data}...");
        
        var context = new AddHashContext(message.Data);
        await commandBuilder.ExecuteAsync(context);

        logger.LogInformation($"Processed {message.Data}...");
    }
}