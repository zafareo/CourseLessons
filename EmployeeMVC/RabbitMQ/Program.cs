
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "10.10.1.181",
    UserName = "Javlon",
    Password = "Javlon"
};

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
//channel.QueueDeclare("employee", exclusive: false);
Console.WriteLine(" Listenning ");
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Zafar received a new message: {message}");
};


channel.BasicConsume(queue: "Sent from Zafar", autoAck: true, consumer: consumer);

Console.ReadKey();