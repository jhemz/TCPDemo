using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPF_TCP_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private vmMain vmMain;
        private TcpClient client;
        public MainWindow()
        {
            InitializeComponent();

            vmMain = new vmMain();
            DataContext = vmMain;
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            //create client
            client = new TcpClient();

            //get local ip address
            List<string> IPAddresses = new List<string>();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] ip = host.AddressList;

            for (int j = 0; j < ip.Length; j++)
            {
                if (ip[j].ToString().Contains("."))
                {
                    IPAddresses.Add(ip[j].ToString());
                }
            }

            //connect
            await client.ConnectAsync(IPAddresses.First(), 100);
            vmMain.DisplayMessage = "Connected!";

        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            client.Close();
            vmMain.DisplayMessage = "Disconnected!";
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if(client != null)
            {
                //create stream
                NetworkStream stream = client.GetStream();

                //convert string message to byte array
                byte[] messageBytes = Encoding.Default.GetBytes(vmMain.Message);

                //write to the listener
                stream.Write(messageBytes, 0, messageBytes.Length);
                vmMain.DisplayMessage = "Message Sent!";
            }
           
        }

       
    }
}
