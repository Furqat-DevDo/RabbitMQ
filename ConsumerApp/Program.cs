using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("RabbitMQ Consumer Application Started.");

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

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" Received new message : {message}");
};

channel.BasicConsume(queue: "booking",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press enter to exit.");
Console.ReadLine();