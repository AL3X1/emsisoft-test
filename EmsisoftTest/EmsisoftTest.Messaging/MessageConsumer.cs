using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Messaging.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmsisoftTest.Messaging;

public class MessageConsumer : IMessageConsumer, IDisposable
{
    private readonly QueueSettings _queueSettings;

    private readonly IModel _channel;

    public MessageConsumer(AppSettings appSettings)
    {
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

    public void StartConsuming(EventHandler<BasicDeliverEventArgs> messageReceivedAction)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += messageReceivedAction;
        _channel.BasicConsume(consumer, _queueSettings.Name);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}