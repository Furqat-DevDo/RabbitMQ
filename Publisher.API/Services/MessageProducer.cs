using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace RabbitMQ.Publisher.API.Services;

public class MessageProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "myPassword",
            VirtualHost = "/"
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "booking",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("","booking",body : body);
    }
}