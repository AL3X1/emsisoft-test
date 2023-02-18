using System.Text;
using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmsisoftTest.Messaging;

public class MessageConsumer : IMessageConsumer, IDisposable
{
    private readonly QueueSettings _queueSettings;

    private readonly IModel _channel;

    private readonly ILogger<MessageConsumer> _logger;

    public MessageConsumer(AppSettings appSettings, ILogger<MessageConsumer> logger)
    {
        _logger = logger;
        _queueSettings = appSettings.Queue;
        
        var factory = new ConnectionFactory
        {
            HostName = _queueSettings.Host,
            Port = _queueSettings.Port,
            UserName = _queueSettings.Username, 
            Password = _queueSettings.Password
        };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueBind(_queueSettings.Name, _queueSettings.Exchange, string.Empty);
    }

    public void StartConsuming(Func<string, Task> messageReceivedAction)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, args) =>
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Received: {message}");
            await messageReceivedAction(message);
        };
        
        _channel.BasicConsume(consumer, _queueSettings.Name);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}