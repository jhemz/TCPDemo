using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole_TCP_Server
{
    class Program
    {
        private static TcpListener server;

        static async Task Main(string[] args)
        {
            while (true){

                await SetupServerAsync();
            }
        }
        private static async Task SetupServerAsync()
        {
            Console.Write("Starting server.." + Environment.NewLine);
            server = new TcpListener(IPAddress.Any, 100);

            while (true)
            {
                server.Start();
                Console.Write("Server listener socket opened, listening for incoming connections.." + Environment.NewLine);
                Socket socket = await server.AcceptSocketAsync();
                Console.Write("Connection made, reading data.." + Environment.NewLine);
                //set timeout
                socket.ReceiveTimeout = 30000;

                byte[] b = new byte[10000];

                //initial read
                int k = socket.Receive(b);

                while (k != 0)
                {
                    string data = Encoding.Default.GetString(b, 0, k);
                    Console.Write(data + Environment.NewLine);



                    //re-read while data is available
                    k = socket.Receive(b);
                }

                socket.Close();
                Console.Write("Server listener socket closed.." + Environment.NewLine);
            }
            
        }
    }
}
