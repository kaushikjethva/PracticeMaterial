using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DirectSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0] + "------" + args[1]);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "dir-exch",
                type: ExchangeType.Direct,
                durable: false,
                autoDelete: false,
                arguments: null);

            while (true)
            {
                //Console.Write("Enter the routing key (orders, basket, payment):");
                //var routingKey = Console.ReadLine();
                //Console.Write("Enter the message(Empty to Exit");
                //var message = Console.ReadLine();
                //if (string.IsNullOrEmpty(message))
                //{
                //    break;
                //}

                channel.QueueDeclare(args[0], durable: false, exclusive: false, autoDelete: false);                
                                
                channel.QueueBind(args[0], "dir-exch", args[1], null);
                //channel.QueueBind("basket-q", "dir-exch", "basket", null);
                //channel.QueueBind("payment-q", "dir-exch", "payment", null);

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
}
