namespace EmsisoftTest.Messaging.Interfaces;

public interface IMessageConsumer
{
    void StartConsuming(Func<string, Task> messageReceivedAction);
}