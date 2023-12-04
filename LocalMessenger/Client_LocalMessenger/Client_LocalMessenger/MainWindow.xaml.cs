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


namespace Client_LocalMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string address = string.Empty;
        int port = 0;


        public MainWindow()
        {
            InitializeComponent();
			WaitForServerName();
			
		}

		private void WaitForServerName()
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

			}
			catch (Exception ex)
			{
				if (ex.Message == "Only one usage of each socket address (protocol/network address/port) is normally permitted.")
				{
					MessageBox.Show("Зачекайте будь ласка пробує підєднатись інший користувач");
				}
				else
				{
					MessageBox.Show(ex.Message);
				}

			}
		}


	}
}