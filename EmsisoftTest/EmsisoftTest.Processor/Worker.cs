using System.Text;
using EmsisoftTest.Messaging.Interfaces;
using RabbitMQ.Client.Events;

namespace EmsisoftTest.Processor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMessageConsumer _messageConsumer;
    
    public Worker(ILogger<Worker> logger, IMessageConsumer messageConsumer)
    {
        _logger = logger;
        _messageConsumer = messageConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageConsumer.StartConsuming(OnMessageReceived);
    }

    private void OnMessageReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
            
        _logger.LogInformation($"Received: {message}");
    }
}