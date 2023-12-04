using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using System.Windows.Interop;
using System.Runtime.Serialization.Formatters.Binary;
using Data_LocalMessenger;
using System.IO;

namespace Client_LocalMessenger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
    {
        string address = string.Empty;
        int port = 0;

		TcpClient socket = null;
		BinaryFormatter formatter = null;
		NetworkStream networkStream = null;

		public MainWindow()
        {
            InitializeComponent();
			formatter= new BinaryFormatter();

		}



		private void WaitForServer_Button_Click(object sender, RoutedEventArgs e)
		{
			btStart.Visibility = Visibility.Hidden;
			while (true)
			{
				try
				{
					UdpClient receiver = new UdpClient(8888);

					IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
					byte[] datagram = receiver.Receive(ref endPoint);
					string[] msg = Encoding.UTF8.GetString(datagram).Split(' ');

					receiver.Close();

					address = msg[0];
					port = int.Parse(msg[1]);

					if (!string.IsNullOrEmpty(address) && port != 0)
					{
						
						btGoLogin.Visibility = Visibility.Visible;
					}
					break;

				}
				catch (Exception ex)
				{
					if (ex.Message == "Only one usage of each socket address (protocol/network address/port) is normally permitted.")
					{
						tbWait.Text = "Зачекайте будь ласка пробує підєднатись інший користувач";
					}
					else
					{
						MessageBox.Show(ex.Message);
					}

				}
			}
		}

		private void btLogin_Click(object sender, RoutedEventArgs e)
		{
			socket = new TcpClient();
			socket.Connect(address, port);
			networkStream = socket.GetStream();

			Message message = new Message() 
			{ 
				From = tbLogin.Text.Trim(),
				To = string.Empty,
				Command = CommandMessage.Login,
				TextMessage = string.Empty,
				FileName = string.Empty,
				FileData = null,
				CreatedAt = DateTime.Now,
			};
			SendMessage(message);

		}
		private void SendMessage(Message message)
		{
			try
			{
				using (MemoryStream ms = new MemoryStream())
				{
					formatter.Serialize(ms, message);
					networkStream.Write(ms.ToArray(), 0, (int)ms.Length);
					networkStream.Flush();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Send message: " + ex.Message + "\r\n" + ex.StackTrace);
			}
		}
	}
}