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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Client_LocalMessenger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
    {
		string userLogin = string.Empty;
		string address = string.Empty;
        int port = 0;

		TcpClient socket = null;
		BinaryFormatter formatter = null;
		NetworkStream networkStream = null;

		string curentUser = string.Empty;
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


		private void ReceiveResponce()
		{
			try
			{
				while (true)
				{
					StreamReader sr = new StreamReader(networkStream, Encoding.UTF8);
					Message response = (Message)formatter.Deserialize(sr.BaseStream);

					switch (response.Command)
					{
						case CommandMessage.Refresh:
							if (response == null)
								throw new Exception("No responce for server");

							if (userLogin == string.Empty)
								Dispatcher.Invoke(() => userLogin = tbLogin.Text.Trim());

							CreateUsersUI(response.TextMessage);
							break;

						case CommandMessage.Negative:
							//HistoryAllMessages responseU = (HistoryAllMessages)formatter.Deserialize(sr.BaseStream);

							//foreach (string str in responseU.histories.Select(h => h.Message))
							//{
							//	Label label = new Label() { Content = str };
							// Dispatcher.Invoke(()=>	place.Children.Add(label));
							//}
							//action = "";
							break;
						case CommandMessage.ReturnHistory:
							if (response.To == curentUser || response.From == curentUser)
								CreatePlaceUI(response.HistoryMessages);
						break;
						//case "CreateMessage":
						//	//HistoryAllMessages response = (HistoryAllMessages)formatter.Deserialize(sr.BaseStream);

						//	//foreach (string str in response.histories.Select(h => h.Message))
						//	//{
						//	//	Label label = new Label() { Content = str };
						//	//	Dispatcher.Invoke(() => place.Children.Add(label));
						//	//}
						//	//action = "";
						//	break;
					}
				}
				

			}
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btLogin_Click(object sender, RoutedEventArgs e)
		{
			socket = new TcpClient();
			socket.Connect(address, port);
			networkStream = socket.GetStream();

			try
			{
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

				Task.Run(ReceiveResponce);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Client Login\n" + ex.Message);
			}
		

		}

		private void SendMessage(Message message)
		{

			try
			{
				using (MemoryStream ms = new MemoryStream())
				{
					formatter.Serialize(ms, message);
					networkStream = socket.GetStream();
					networkStream.Write(ms.ToArray(), 0, (int)ms.Length);
					networkStream.Flush();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Send message: " + ex.Message + "\r\n" + ex.StackTrace);
			}
		}


		#region Decoration
		private void CleanTextBlock_GotFocus(object sender, RoutedEventArgs e)
		{
			if((sender as TextBox).Text=="Введіть повідомлення...")
			{
				(sender as TextBox).Text = "";
			}
			
		}
		private void AddTextBlock_LostFocus(object sender, RoutedEventArgs e)
		{
			if ((sender as TextBox).Text == "")
			{
				(sender as TextBox).Text = "Введіть повідомлення...";
			}
		}
		#endregion


		private void CreateMessageButton_Click(object sender, RoutedEventArgs e)
		{
			
			if (tbSend.Text == "" || tbSend.Text == "Введіть повідомлення...")
			{
				return;
			}
			
			Message message = new Message()
			{
				From = userLogin,
				To= curentUser,
				Command = CommandMessage.Send,
				TextMessage = tbSend.Text,
				CreatedAt = DateTime.Now,
			};
			SendMessage(message);
			
		}
		private void GetUserAction(object sender, MouseButtonEventArgs e)
		{
			curentUser = (sender as ListBoxItem).Tag.ToString();
			Message message = new Message()
			{
				From = userLogin,
				To = curentUser,
				Command = CommandMessage.ReturnHistory,
				TextMessage = string.Empty,
				CreatedAt = DateTime.Now,
			};
			SendMessage(message);
		}


		#region UI
		private void CreateUsersUI(string users)
		{
			Dispatcher.Invoke(() => lbUsers.Items.Clear());
			foreach (string user in users.Split('&'))
			{
				if (user != "" && user != userLogin)
				{
					Dispatcher.Invoke(() => {
						ListBoxItem item = new ListBoxItem() { Tag = user };
						item.MouseLeftButtonUp += GetUserAction;
						Border border = new Border() {Height = 60, Width = 200, Margin = new Thickness(5), Background = Brushes.AliceBlue, CornerRadius = new CornerRadius(10) };
						TextBlock textBlock = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Text = user };
						border.Child = textBlock;
						item.Content = border;
						lbUsers.Items.Add(item);
					});
				}
			}
		}

		private void CreatePlaceUI(List<HistoryMessages> history)
		{
			Dispatcher.Invoke(() =>
			{
				place.Children.Clear();
			});
			foreach (HistoryMessages message in history)
			{
				Dispatcher.Invoke(() =>
				{
					Label label = new Label() { Content = message.Message };
					place.Children.Add(label);
				});
				
			}
		}

		#endregion

		private void Window_Closing(object sender, CancelEventArgs e)
		{
		
			Message message = new Message()
			{
				From = userLogin,
				To = "Server",
				Command = CommandMessage.End,
				TextMessage = "Bye",
				CreatedAt = DateTime.Now,
			};
			SendMessage(message);
			networkStream.Close();
			socket.Close();
		
		}
	}
}