using System.Text;
using System.Text.Json;
using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EmsisoftTest.Messaging;

public class MessageProducer : IMessageProducer, IDisposable
{
    private readonly ILogger<MessageProducer> _logger;

    private readonly QueueSettings _queueSettings;
    
    private readonly IModel _channel;

    public MessageProducer(AppSettings appSettings, ILogger<MessageProducer> logger)
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

    public void Produce<T>(MessagePayload<T> message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
        
        _channel.BasicPublish(exchange: _queueSettings.Exchange, routingKey: string.Empty, body: messageBytes);
        _logger.LogInformation($"Message sent - {jsonMessage}");
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}