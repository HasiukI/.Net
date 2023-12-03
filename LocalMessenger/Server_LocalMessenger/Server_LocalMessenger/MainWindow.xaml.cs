using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server_LocalMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpListener listener = null;
        string address = string.Empty;
        int port = 8888;


        Semaphore semaphore = new Semaphore(3, 10);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var host = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (var ip in host)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        address = ip.ToString();
                        break;
                    }
                }
                Task.Run(SendIP);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Server Listen click "+ex.Message);
            }
        }

        private async Task SendIP()
        {
            int broadcastPort = 8888;
            UdpClient transferIp =  new UdpClient();
            transferIp.EnableBroadcast = true;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, broadcastPort);
            byte[] datagram = Encoding.UTF8.GetBytes(address + " " + port);
            
            while (true)
            {
                await transferIp.SendAsync(datagram, datagram.Length, endPoint);
                System.Threading.Thread.Sleep(10000);
            }
        }



       
    }
}
