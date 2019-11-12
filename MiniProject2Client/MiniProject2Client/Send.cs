﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MiniProject2Client
{
    class Send
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "CarRental",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("please type in the desicred car type with a date after: Audi 22/06/1996");
                string message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);  //made to bytes


                channel.BasicPublish(exchange: "",
                                     routingKey: "CarRental",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
