using RabbitMQ.Client.Events;

namespace EmsisoftTest.Messaging.Interfaces;

public interface IMessageConsumer
{
    void StartConsuming(EventHandler<BasicDeliverEventArgs> messageReceivedAction);
}