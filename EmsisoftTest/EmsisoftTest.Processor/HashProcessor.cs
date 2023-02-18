using System.Text.Json;
using EmsisoftTest.Domain.Commands;
using EmsisoftTest.Infrastructure.Interfaces;
using EmsisoftTest.Messaging;
using EmsisoftTest.Messaging.Interfaces;

namespace EmsisoftTest.Processor;

public class HashProcessor : BackgroundService
{
    private readonly ILogger<HashProcessor> _logger;
    private readonly IMessageConsumer _messageConsumer;
    private readonly ICommandBuilder _commandBuilder;
    
    public HashProcessor(ILogger<HashProcessor> logger, IMessageConsumer messageConsumer, ICommandBuilder commandBuilder)
    {
        _logger = logger;
        _messageConsumer = messageConsumer;
        _commandBuilder = commandBuilder;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageConsumer.StartConsuming(ProcessMessageAsync);
    }
    
    private async Task ProcessMessageAsync(string message)
    {
        try
        {
            var payload = JsonSerializer.Deserialize<MessagePayload<string>>(message);

            if (payload == null)
            {
                _logger.LogInformation("Could not process empty payload");
                return;
            }
            
            var context = new AddHashContext(payload.Data);
            await _commandBuilder.ExecuteAsync(context);
        }
        catch(Exception e)
        {
            _logger.LogCritical(e.ToString());
        }
    }
}