using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvanchSubscriberDemo
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

            var arguments = new Dictionary<string, object>()
            {
                {"x-message-ttl",6000},     //TTL for the Queue messages (all messages)
                {"x-expires", 30*60*1000 }        //Queue expiry after ideal timeout, in mili second
            };

            channel.QueueDeclare("demoq", durable: false, exclusive: false, autoDelete: false, arguments: arguments);
            
            channel.QueueBind("demoq", "demo-exch", "", null);

            var Consumer = new EventingBasicConsumer(channel);
            Consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"Message Received:{message}");
                channel.BasicAck(ea.DeliveryTag, multiple: false);      //Explicit Ack.
            };

            channel.BasicConsume(queue: "demoq", autoAck: false, consumer: Consumer);

            Console.WriteLine("Waiting for Messages... Press ENTER to Exit");
            Console.ReadLine();

            channel.Dispose();
            connection.Dispose();
        }
    }
}
