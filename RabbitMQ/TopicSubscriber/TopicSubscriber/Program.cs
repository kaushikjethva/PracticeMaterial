using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace TopicSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("Invalid Argukment");
            }

            Console.WriteLine($"Queye Name:{ args[0]}, -- Key: " + args[0]);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic-exch",
                type: ExchangeType.Topic,
                durable: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare(args[0], durable: false, exclusive: false, autoDelete: false);

            var keys = args.Skip(1).Take(args.Length - 1);
            foreach (var key in keys)
            {
                channel.QueueBind(args[0], "topic-exch", key, null);
            }            

            var Consumer = new EventingBasicConsumer(channel);
            Consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"Message Received:{message}");
            };

            channel.BasicConsume(args[0], true, Consumer);

            Console.WriteLine("Waiting for Messages... Press ENTER to Exit");
            Console.ReadLine();

            channel.Dispose();
            connection.Dispose();

        }
    }    
}
