using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniProject2Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form = new Form1();
            Application.Run(form);


            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 6789);

            //TcpListener serverSocket = new TcpListener(6789);
            serverSocket.Start();
         

            while (true)
            {
                using (TcpClient connectionSocket = serverSocket.AcceptTcpClient())
                {
                    Console.WriteLine("Server activated now");
                    Service service = new Service(connectionSocket);
                    //Use Task and delegates

                    //Task solution with delegates
                    Task.Factory.StartNew(() => service.DoIt());

                    //OR
                    //Task.Factory.StartNew(service.DoIt);

                    //OR
                    //Task.Run(( ) => service.DoIt());

                    //OR
                    //Thread solution
                    //Thread myThread = new Thread(new ThreadStart(service.DoIt));

                    //OR
                    //Thread myThread = new Thread(service.DoIt);
                    //myThread.Start();

                }
            }
        }
    }
}
