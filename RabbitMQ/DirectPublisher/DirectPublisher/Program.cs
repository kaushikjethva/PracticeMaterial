using System;
using System.Text;
using RabbitMQ.Client;

namespace DirectPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
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
                Console.Write("Enter the routing key (orders, basket, payment):");
                var routingKey = Console.ReadLine();
                Console.Write("Enter the message(Empty to Exit");
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    break;
                }

                //channel.QueueDeclare("order-q");
                //channel.QueueDeclare("basket-q");
                //channel.QueueDeclare("payment-q");

                //channel.QueueBind("order-q", "dir-exch", "orders", null);
                //channel.QueueBind("basket-q", "dir-exch", "basket", null);
                //channel.QueueBind("payment-q", "dir-exch", "payment", null);

                var payload = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "dir-exch",
                    routingKey: routingKey,
                    mandatory: false,
                    basicProperties: null,
                    body: payload);
            }
        }
    }
}
