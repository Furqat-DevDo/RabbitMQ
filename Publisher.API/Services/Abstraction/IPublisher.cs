namespace RabbitMQ.Publisher.API.Services;

public interface IPublisher 
{
    public void PublishMessage<T>(string queueName, T message);
}