using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject2Server
{
    class Receive
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "CarRental1",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);  //"toyota 22/07/1996"

                    //EIP - Splitter
                    String[] messageArray = message.Split(' ');
                    string carType = messageArray[0];
                    DateTime dateNotDateTime = DateTime.ParseExact(messageArray[1], "dd/MM/yyyy", null);
                    Console.WriteLine("test af datetime " + dateNotDateTime);



                    //
                    //
                    //


                };
                channel.BasicConsume(queue: "CarRental",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
