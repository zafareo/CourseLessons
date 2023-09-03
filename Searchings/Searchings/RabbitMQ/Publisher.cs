using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchings.RabbitMQ
{
    public static class Publisher
    {
        public static void SendMessage()
        {
            var factory = new ConnectionFactory
            { 
                HostName = "localhost",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            const string message = " What color is your bugatti? ";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "BottomG",
                                 routingKey: "money",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
