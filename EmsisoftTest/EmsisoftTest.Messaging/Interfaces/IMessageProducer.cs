namespace EmsisoftTest.Messaging.Interfaces;

public interface IMessageProducer
{
    void Produce<T>(MessagePayload<T> message);
}