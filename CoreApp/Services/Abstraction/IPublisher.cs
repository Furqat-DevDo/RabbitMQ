namespace CoreApp.Services.Abstraction;

public interface IPublisher 
{
    public void PublishMessage<T>(string queueName, T message);
}