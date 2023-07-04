namespace RabbitMQ.Publisher.API.Services;

public interface IMessageProducer
{
    public void SendMessage<T> (T message);
}