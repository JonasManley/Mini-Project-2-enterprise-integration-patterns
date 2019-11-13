using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace MiniProject2Server
{
    class Receive
    { 
        //Aggregation fields 
        private static string message1;  // Type and Date
        private static string message2;  // Color
        private static string message3;  // Driver name and license  
        private static string logPath = @"C:\Users\Jonas\source\repos\Mini-Project-2-enterprise-integration-patterns\Log.txt";


        public static void Main()
        {
            //EIP Channel created on localhost with help of MqRabbit. 
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Declaring of the que to send messages to. 
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
                List<Car> chooseCarList = new List<Car>();
                Car selectedCar = null;
                bool found = false;


                //Message recevied
                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    
                    var message = Encoding.UTF8.GetString(body).ToString();
                    Console.WriteLine(" [.] Message send from client", message);

                    File.AppendAllText(logPath, TimeStampForLog(message));
                    //write message down into a TXT log file (not made yet) 

                    switch (caseSwitch)
                    {
                        case 1:
                            message1 = message;
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
                            message2 = message;
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
                            if(response == string.Empty)
                            {
                                response = "Color not found, please try with another one or check spelling";
                            }
                            else
                            {
                                var responseBytesCase2 = Encoding.UTF8.GetBytes(response);
                                channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                                  basicProperties: replyProps, body: responseBytesCase2);
                                channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                  multiple: false);
                                caseSwitch += 1;
                            }
                            break;
                        case 3:
                            Console.WriteLine("case 3 - choose car");
                            foreach (var car in availableCars)
                            {
                                if(car.Color == message)
                                {
                                    availableCarsByColor.Add(car);
                                }
                            }
                            foreach (var car in availableCarsByColor)
                            {
                                response = response + car.ToString() + "-";
                                
                                chooseCarList.Add(car);
                            }

                            var responseBytesCase3 = Encoding.UTF8.GetBytes(response);
                            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                              basicProperties: replyProps, body: responseBytesCase3);
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                              multiple: false);
                            caseSwitch += 1;
                            break;
                        case 4:
                            Console.WriteLine("case 4 - selecet car");
                            int index = Convert.ToInt32(message)-1;
                            if(index < 0 || availableCarsByColor.Count < index+1 )
                            {
                                response = "The number is not valid, try with a differnt one";
                            }
                            else
                            {
                                selectedCar = chooseCarList[index];
                                response = "selected CAR: " + selectedCar.ToString();
                            }
                            
                            var responseBytesCase4 = Encoding.UTF8.GetBytes(response);
                            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                              basicProperties: replyProps, body: responseBytesCase4);
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                              multiple: false);
                            caseSwitch += 1;
                            break;
                        case 5:
                            message3 = message;
                            string[] nameAndLicense = message.Split(' ');
                            string name = nameAndLicense[0];
                            string license = nameAndLicense[1];
                            Driver driver = new Driver(name, license);
                            Booking booking = new Booking(driver, selectedCar, DateTime.Now);


                            //Aggregater 
                            response =  message1 + message2 + message3;

                            //Saves informations in a TXT file (illustrate database) 
                            File.WriteAllText(@"C:\Users\Jonas\source\repos\Mini-Project-2-enterprise-integration-patterns\CompletedRentals.txt", response);

                            var responseBytesCase5 = Encoding.UTF8.GetBytes(response);
                            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                              basicProperties: replyProps, body: responseBytesCase5);
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                              multiple: false);

                            
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

        private static string TimeStampForLog(string message)
        {
            string response = DateTime.Now + " " + message + Environment.NewLine; ;
            return response;
        }
    }
}
