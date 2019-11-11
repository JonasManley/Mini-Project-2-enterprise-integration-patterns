using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniProject2Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
                TcpClient clientSocket = new TcpClient("172.20.10.2", 6789);  //Clientens TCP opretter forbindelse til Serverens, ved brug af serverens
                                                                              //IP-adresse 

                Stream ns = clientSocket.GetStream();        //provides a Stream
                StreamReader sr = new StreamReader(ns);      //Et streamReader objekt laves (kan læse information)
                StreamWriter sw = new StreamWriter(ns);      //Et StreamWriter objekt laves (kan skrive information) 

                sw.AutoFlush = true;                         //enable automatic flushing (hvis ikke dette var enablet, så skulle hele
                                                             //StreamWriterens "buffer" være fyldt, før man er i stand til at sende informationen.
            labelStart.Text = "Client has started";
                
                for (int i = 0; i < 5; i++)
                {
                    string message = Console.ReadLine();        //En variable message, der gives alt det der skrives i console vinduet som værdi. 

                    sw.WriteLine(message);                      //Hvad sker helt præcist her? 
                    string serverAnswer = sr.ReadLine();

                    Console.WriteLine("Server: " + serverAnswer);

                }

                Console.WriteLine("No more from server. Press Enter");
                Console.ReadLine();

                ns.Close();

                clientSocket.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
           
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
