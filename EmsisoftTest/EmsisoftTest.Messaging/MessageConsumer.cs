using System.Text;
using System.Text.Json;
using EmsisoftTest.Infrastructure.Configurations;
using EmsisoftTest.Messaging.Interfaces;
using RabbitMQ.Client;

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

    public List<MessagePayload<T>> FetchMessages<T>()
    {
        var messages = new List<MessagePayload<T>>();
        var count = _channel.MessageCount(_queueSettings.Name);

        while (messages.Count < count)
        {
            var payload = FetchMessage<T>();

            if (payload != null)
            {
                messages.Add(payload);
            }   
        }

        return messages;
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
    
    private MessagePayload<T>? FetchMessage<T>()
    {
        var result = _channel.BasicGet(_queueSettings.Name, true);

        if (result == null)
        {
            return null;
        }
        
        var body = result.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        
        return JsonSerializer.Deserialize<MessagePayload<T>>(message);
    }
}