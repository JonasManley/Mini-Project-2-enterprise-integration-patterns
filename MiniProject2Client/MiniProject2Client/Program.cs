using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject2Client
{

    class Program
    {
        static void Main(string[] args)
        {
            var Client = new Client();

            Console.WriteLine("Car Rental");
            Console.WriteLine("Type what car you want and a date to see availability, like: audi 22/01/1996");
            var inputCar = Console.ReadLine();
            Console.WriteLine($" [x] searching for: {inputCar}");
            var responseAvailable = Client.Call(inputCar);
            Console.WriteLine($" [.] Got: '{responseAvailable}'");
            Console.WriteLine("---------------------------------------------------------------------------------");
            var responseColor = Client.Call(" ");
            Console.WriteLine($" [.] Colors available: {responseColor}");
            Console.WriteLine("Write the name of the color you want:");
            var inputColor = Console.ReadLine();
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine($" [x] Collection the car(s) with the color: {inputColor}");
            var responeCarColor = Client.Call(inputColor);
            Console.WriteLine($" [.] Got: '{responeCarColor}'");


            Client.Close();

            Console.ReadLine();
        }
    }
}
