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
            var input = Console.ReadLine();
            Console.WriteLine($" [x] searching for: {input}");
            var response = Client.Call(input);
            Console.WriteLine($" [.] Got: '{response}'");

            Console.WriteLine("Type what car you want and a date to see availability, like: audi 22/01/1996");
            var input1 = Console.ReadLine();
            Console.WriteLine($" [x] searching for: {input1}");
            var response1 = Client.Call(input);
            Console.WriteLine($" [.] Got: '{response1}'");


            Client.Close();

            Console.ReadLine();
        }
    }
}
