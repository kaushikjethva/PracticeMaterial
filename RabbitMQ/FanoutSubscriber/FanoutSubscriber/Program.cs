using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace FanoutSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Queye Name -- " + args[0]);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "fanout-exch",
                type: ExchangeType.Fanout,
                durable: false,
                autoDelete: false,
                arguments: null);         
                
                channel.QueueDeclare(args[0], durable: false, exclusive: false, autoDelete: false);

                channel.QueueBind(args[0], "fanout-exch", "", null);                

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
