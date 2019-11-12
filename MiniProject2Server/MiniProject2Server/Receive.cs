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
                channel.QueueDeclare(queue: "rpc_queue", durable: false,
                  exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "rpc_queue",
                  autoAck: false, consumer: consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

                //Variable used within the Received protocol. 
                int caseSwitch = 1;
                List<Car> availableCars = new List<Car>();
                List<string> colorsFound = new List<string>();
                List<Car> availableCarsByColor = new List<Car>();
                bool found = false;

                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    
                    var message = Encoding.UTF8.GetString(body).ToString();
                    Console.WriteLine(" [.] Message send from client", message);
                    //write message down into a TXT log file (not made yet) 
                   
                    switch (caseSwitch)
                    {
                        case 1:
                            //Check avaliablity
                            Console.WriteLine("Case 1 - Check avaliablity");

                            //EIP - Splitter
                            String[] messageArray = message.Split(' ');
                            string carType = messageArray[0];
                            string date = messageArray[1];
                            //EIP - Splitter

                            DataStorage dataStorage = new DataStorage();
                            foreach (var car in dataStorage.CarList)
                            {
                                if (car.Type == carType && car.Date == date)
                                {
                                    found = true;
                                    availableCars.Add(car);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if(found == true)
                            {
                                response = "bil blev fundet";
                                Console.WriteLine("bil blev fundet");

                                var responseBytes = Encoding.UTF8.GetBytes(response);
                                channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                                  basicProperties: replyProps, body: responseBytes);
                                channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                  multiple: false);

                                caseSwitch += 1;
                            }
                            else
                            {
                                response = "bil blev ikke fundet";
                                Console.WriteLine("bil blev ikke fundet");

                                var responseBytes = Encoding.UTF8.GetBytes(response);
                                channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                                  basicProperties: replyProps, body: responseBytes);
                                channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                  multiple: false);
                            }
                            break;
                        case 2:
                            Console.WriteLine("case 2 - choose color");
                            //Choose color 
                            foreach (var car in availableCars)
                            {
                                if (colorsFound.Contains(car.Color))
                                {
                                    continue;
                                }
                                else
                                {
                                    colorsFound.Add(car.Color);
                                }
                            }

                            foreach (var color in colorsFound)
                            {
                                response = response + " " + color;
                            }

                            var responseBytesCase2 = Encoding.UTF8.GetBytes(response);
                            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                              basicProperties: replyProps, body: responseBytesCase2);
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                              multiple: false);
                            caseSwitch += 1;
                            break;
                        case 3:
                            foreach (var car in availableCars)
                            {
                                if(car.Color == message)
                                {
                                    availableCarsByColor.Add(car);
                                }
                            }
                            foreach (var car in availableCarsByColor)
                            {
                                response = response + " " + car.ToString();
                            }

                            var responseBytesCase3 = Encoding.UTF8.GetBytes(response);
                            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                              basicProperties: replyProps, body: responseBytesCase3);
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                              multiple: false);
                            caseSwitch += 1;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
