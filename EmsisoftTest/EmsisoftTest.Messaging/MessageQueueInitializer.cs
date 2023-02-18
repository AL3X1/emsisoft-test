using EmsisoftTest.Infrastructure.Configurations;
using RabbitMQ.Client;

namespace EmsisoftTest.Messaging;

public abstract class MessageQueueInitializer
{
    public static void Initialize(QueueSettings queueSettings)
    {
        var factory = new ConnectionFactory
        {
            HostName = queueSettings.Host,
            Port = queueSettings.Port,
            UserName = queueSettings.Username, 
            Password = queueSettings.Password
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queueSettings.Name, true, false, false);
        channel.ExchangeDeclare(queueSettings.Exchange, ExchangeType.Direct, durable: true, autoDelete: false);
    }
}