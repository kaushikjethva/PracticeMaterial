﻿using System;
using System.Text;
using RabbitMQ.Client;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "csq", durable: false, exclusive: false, autoDelete: false, arguments: null);

                        while(true)
                    {
                        Console.Write("Enter the message (Empty to exit);");
                        var message = Console.ReadLine();
                        if(string.IsNullOrEmpty(message))
                        {
                            break;
                        }
                        var payload = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "",
                            routingKey: "csq",
                            basicProperties: null,
                            body: payload);

                    }
                }
                
        }
        }
    }
}
