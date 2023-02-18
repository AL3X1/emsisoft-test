namespace EmsisoftTest.Messaging.Interfaces;

public interface IMessageConsumer
{
    List<MessagePayload<T>> FetchMessages<T>();
}