using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("RabbitMQ Consumer Application Started.");

var factory = new ConnectionFactory 
{
    HostName = "localhost" ,
    UserName = "user",
    Password = "myPassword",
    VirtualHost = "/"
}; 


using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

string[] queueNames = { "files", "booking" };

foreach (var queueName in queueNames)
{

    channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (sender, eventArgs) =>
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Message sender  : {sender}");
        Console.WriteLine($"Received message: {message} from queue: {queueName}");
        channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
    };

    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
}

Console.ReadLine();
