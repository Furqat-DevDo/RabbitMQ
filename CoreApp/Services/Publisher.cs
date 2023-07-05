using System.Text;
using System.Text.Json;
using CoreApp.Services.Abstraction;
using RabbitMQ.Client;

namespace CoreApp.Services;

public class Publisher : IPublisher
{
    public void PublishMessage<T>(string queueName, T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "user",
            Password = "myPassword"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Direct);
        channel.QueueBind(queue: queueName, exchange: "logs", routingKey: queueName);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish(exchange: "logs", routingKey: queueName, basicProperties: null, body: body);

        Console.WriteLine($"Message published to queue '{queueName}' successfully.");
    }
}