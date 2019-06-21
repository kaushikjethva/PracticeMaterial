using RabbitMQ.Client;
using System;
using System.Text;

namespace AdvancePublisherDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "demo-exch",
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: null);


            while (true)
            {
                Console.Write("Enter the message(Empty to Exit");
                var message = Console.ReadLine();

                if (string.IsNullOrEmpty(message))
                {
                    break;
                }
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                props.ContentType = "text/plain";
                //props.Expiration = "15000";     //TTL per message
                var payload = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "demo-exch",
                    routingKey: "",
                    mandatory: false,
                    basicProperties: null,
                    body: payload);
            }
            channel.Dispose();
            connection.Dispose();
        }
    }
}
