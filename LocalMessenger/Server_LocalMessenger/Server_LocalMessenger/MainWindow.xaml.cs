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
using System.Xml.Linq;

namespace Server_LocalMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpListener listener = null;
		Semaphore semaphore = new Semaphore(3, 10);

		BinaryFormatter formatter = null;

        string address = string.Empty;
        int port = 8888;

        List<User> users;
		List<History> history;

		public MainWindow()
        {
            InitializeComponent();
            formatter = new BinaryFormatter();
			users = new List<User>();
			history = new List<History>();
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

				listener = new TcpListener(IPAddress.Parse(address), port);
				listener.Start();

				Task.Run(SendIP);
                Task.Run(WaitOneClient);

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
        private async Task WaitOneClient()
        {
            while (true)
            {
				semaphore.WaitOne();
                Task.Run(ListenAsync);
			}
        }
        private async Task ListenAsync()
        {
			try
			{
				TcpClient client = await listener.AcceptTcpClientAsync();
				NetworkStream network = client.GetStream();
				StreamReader reader = null;

				while (true)
				{
					if (!network.DataAvailable) continue;
					reader = new StreamReader(network, Encoding.UTF8);

					Message message = (Message)formatter.Deserialize(reader.BaseStream);

                    switch (message.Command)
                    {
                        case CommandMessage.Login:
                            string usersOnline = CommandLogin(message, client);

							Message toSend = new Message()
							{
								TextMessage = usersOnline,
								To = string.Empty,
								CreatedAt = DateTime.Now,
								From = "Server",
							};

                            if (usersOnline!=null)
							{
								toSend.Command = CommandMessage.Refresh;
								SendGroupMessage(toSend);
							}
							else
							{
								toSend.Command = CommandMessage.Negative;
								SendMessage(toSend, client);
							}
							break;
						case CommandMessage.ReturnHistory:
							History history =  GetHistory(message);

							Message toSendH = new Message()
							{
								TextMessage = "History",
								CreatedAt = DateTime.Now,
								To = message.To,
								From = message.From,
								Command = CommandMessage.ReturnHistory,
								HistoryMessages = history.HistoryMessage, 
							};
							SendMessage(toSendH, client);
							break;
						case CommandMessage.Send:
							List<HistoryMessages> hm=	NewMessage(message);

							Message toSendM = new Message()
							{
								TextMessage = "History",
								CreatedAt = DateTime.Now,
								To = message.To,
								From = message.From,
								Command = CommandMessage.ReturnHistory,
								HistoryMessages = hm,
							};
							SendMessage(toSendM, client);
							SendMessage(toSendM, users.Where(u => u.Name == message.To).FirstOrDefault().Client);
							break;
						case CommandMessage.SendFile:
                            List<HistoryMessages> hf = NewMessage(message);

                            Message toSendF = new Message()
                            {
                                TextMessage = "History",
                                CreatedAt = DateTime.Now,
                                To = message.To,
                                From = message.From,
                                Command = CommandMessage.ReturnHistory,
                                HistoryMessages = hf,
                            };
                            SendMessage(toSendF, client);
                            SendMessage(toSendF, users.Where(u => u.Name == message.To).FirstOrDefault().Client);
                            break;
						case CommandMessage.End:
							DeleteUser(message);
							break;
                    }
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Listen: " + ex.Message);
			}
		}

	

		#region Messages
		private void SendGroupMessage(Message toSend)
		{
			foreach (var user in users)
			{
				if (user.Name.Equals(toSend.From)) continue;
					SendMessage(toSend, user.Client);
			}
		}

		private void SendMessage(Message toSend, TcpClient socket)
		{
			try
			{
				using (MemoryStream ms = new MemoryStream())
				{
					formatter.Serialize(ms, toSend);
					NetworkStream network = socket.GetStream();
					network.Write(ms.ToArray(), 0, (int)ms.Length);
					network.Flush();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("SendMessage: " + ex.Message);
			}
		}

		#endregion


		#region Command
		private void DeleteUser(Message message)
		{
			try
			{
				users.Remove(users.Find(u => u.Name == message.From));
				DeleteUserUI(message.From);

			}
			catch(Exception ex)
			{
				MessageBox.Show("Server DeleteUser\n" + ex.Message);
			}
		
		}

		private List<HistoryMessages> NewMessage(Message message)
		{
			try
			{
				History hm = GetHistory(message); 

				if (hm == null)
				{
					throw new Exception("Object null reference");
				}

				if(message.TextMessage!=string.Empty)
					hm.HistoryMessage.Add(new HistoryMessages() { Message = message.TextMessage, UserName = message.From });
				else
                    hm.HistoryMessage.Add(new HistoryMessages() { FileName = message.FileName, bytes = message.FileData, UserName=message.From });
                return hm.HistoryMessage;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Server NewMessage\n" + ex.Message);
			}
			return null;
		}

		private string CommandLogin(Message message, TcpClient client)
		{
			if (!users.Any(u => u.Name == message.From))
			{
				users.Add(new User() { Name = message.From, Client = client });
				string str = "";

	  		    foreach( string u in users.Select(user => user.Name).ToList())
				{
					str += u+"&";
				}
				AddOnlineUser(message.From);
				return str;
			}

			return null;
		}

		private History GetHistory(Message message)
		{
			try
			{
				History hm1 = history.Where(u1 => u1.User1 == message.From).ToList().Find(u2 => u2.User2 == message.To);
				History hm2 = history.Where(u1 => u1.User2 == message.From).ToList().Find(u2 => u2.User1 == message.To);

				if (hm1 == null && hm2 == null)
				{
					hm1 = new History()
					{
						User1 = message.From,
						User2 = message.To,
						HistoryMessage = new List<HistoryMessages>()
					};
					history.Add(hm1);
					return hm1;
				}
				if (hm1 != null && hm2 == null)
					return hm1;
				if (hm1 == null && hm2 != null)
					return hm2;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Server GetHustory\n" + ex.Message);
			}
			return null;
		}

		#endregion

		#region Decorate
		private void AddOnlineUser(string name)
		{
			Dispatcher.Invoke(() =>
			{
				ListBoxItem item = new ListBoxItem() { Tag = name };
				TextBlock textBlock = new TextBlock() { Text= name, HorizontalAlignment=HorizontalAlignment.Center, VerticalAlignment= VerticalAlignment.Center};
				item.Content = textBlock;
				lbAllUser.Items.Add(item);
			});
		}
		private void DeleteUserUI(string name)
		{
			Dispatcher.Invoke(() =>
			{
				foreach(var item in lbAllUser.Items)
				{
					if((item as ListBoxItem).Tag.ToString() == name)
					{
						lbAllUser.Items.Remove(item);
						break;
					}
				}
			});
		}
        #endregion

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
			(sender as Border).BorderBrush = Brushes.DarkBlue;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.Transparent;
        }

        private void Start(object sender, MouseButtonEventArgs e)
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

                listener = new TcpListener(IPAddress.Parse(address), port);
                listener.Start();

                Task.Run(SendIP);
                Task.Run(WaitOneClient);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Server Listen click " + ex.Message);
            }
        }
    }
}
