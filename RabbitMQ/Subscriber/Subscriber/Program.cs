using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {           
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "csq", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var Consumer = new EventingBasicConsumer(channel);
                    Consumer.Received += (ch, ea) =>
                    {
                        var mesage = Encoding.UTF8.GetString(ea.Body);
                        Console.WriteLine($"Message Received:{mesage}");                       
                                          

                    };
                    channel.BasicConsume(queue: "csq", autoAck: true, consumer: Consumer);
                    Console.WriteLine("Awaiting for message..... Plz enter blank to exit.");
                    Console.ReadLine();
                }
            }
            


        }
    }
}
