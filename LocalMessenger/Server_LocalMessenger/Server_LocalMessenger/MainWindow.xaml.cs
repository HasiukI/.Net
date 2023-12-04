using Data_LocalMessenger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        BinaryFormatter formatter = null;
        string address = string.Empty;
        int port = 8888;


        Semaphore semaphore = new Semaphore(3, 10);

        public MainWindow()
        {
            InitializeComponent();
            formatter = new BinaryFormatter();
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
                tbAddressMain.Text += address;
                tbPortMain.Text += port.ToString();
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

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			listener = new TcpListener(IPAddress.Parse(address), port);
			listener.Start();

			try
			{
				TcpClient client = listener.AcceptTcpClient();
				NetworkStream network = client.GetStream();
				StreamReader reader = null;
                while (true)
                {
                    if (!network.DataAvailable) continue;
                    reader = new StreamReader(network, Encoding.UTF8);

                    Message message = (Message)formatter.Deserialize(reader.BaseStream);
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show("Listen: " + ex.Message);
			}
		}
	}
}
