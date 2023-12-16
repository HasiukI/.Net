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
using Microsoft.Win32;
using Image = System.Windows.Controls.Image;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

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

		string pathToFile = string.Empty;
		string fileName = string.Empty;

		double positionMessage = 0;

        string dirPath = string.Empty;
		List<HistoryMessages> curentFiles;

        Thread thread = null;
        public MainWindow()
        {
            InitializeComponent();
			formatter= new BinaryFormatter();
			curentFiles= new List<HistoryMessages>();

            dirPath = Directory.GetCurrentDirectory();
            dirPath += "\\data\\";
            Directory.CreateDirectory(dirPath);
        }



		private async void WaitForServer_Button_Click(object sender, RoutedEventArgs e)
		{
			Dispatcher.Invoke(()=> btStart.Visibility = Visibility.Hidden);
            tbOpus.Visibility= Visibility.Visible;

            thread = new Thread(EntryTextChange);
            thread.IsBackground= true;
            thread.Start();
            while (true)
			{
				try
				{
					UdpClient receiver = new UdpClient(8888);

					IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
					UdpReceiveResult rez = await receiver.ReceiveAsync();
					byte[] datagram = rez.Buffer;
					string[] msg = Encoding.UTF8.GetString(datagram).Split(' ');

					receiver.Close();

					address = msg[0];
					port = int.Parse(msg[1]);

					if (!string.IsNullOrEmpty(address) && port != 0)
					{

                        Dispatcher.Invoke(() => btGoLogin.Visibility = Visibility.Visible);
                        Dispatcher.Invoke(() => BordLogin.Visibility = Visibility.Visible);
                        break;
                    }
					

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
            bool exeption = false;
            tbExeptionLogin.Text = string.Empty;
            if (tbLogin.Text.Trim().Length<3)
            {
                tbExeptionLogin.Text += "Login to short\n";
                exeption = true;
            }
            if (tbLogin.Text.Trim().Length > 30)
            {
                tbExeptionLogin.Text += "Login to long\n";
                exeption = true;
            }
            if (tbLogin.Text.Trim().IndexOfAny( new char[] { '*', '/', '.', ',', '&', '<', '?', '>', '@', '#', '%' }) !=-1 )
            {
                tbExeptionLogin.Text += "Dont write special symvol\n";
                exeption = true;
            }

            if (exeption)
                return;

            socket = new TcpClient();
            socket.Connect(address, port);
            networkStream = socket.GetStream();
            btLogin.Visibility = Visibility.Hidden;
           
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
            catch (Exception ex)
            {
                MessageBox.Show("Client Login\n" + ex.Message);
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
                            {
                                Dispatcher.Invoke(() => userLogin = tbLogin.Text.Trim());
                                Dispatcher.Invoke(() => BordGoMessanger.Visibility = Visibility.Visible);
                            }
							
							CreateUsersUI(response.TextMessage);
							break;

						case CommandMessage.Negative:
                            Dispatcher.Invoke(() => tbExeptionLogin.Text = "This login have another");
							break;
						case CommandMessage.ReturnHistory:
							if (response.To == curentUser || response.From == curentUser)
								CreatePlaceUI(response.HistoryMessages);
						break;
					}
				}
				

			}
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message);
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


        #region Messages
        private void CreateMessageAction(object sender, RoutedEventArgs e)
        {
            if (tbSend.Text == "" || tbSend.Text == "Write message..." || curentUser == string.Empty)
            {
                return;
            }


            Message message = new Message()
            {
                From = userLogin,
                To = curentUser,
                Command = CommandMessage.Send,
                TextMessage = tbSend.Text,
                CreatedAt = DateTime.Now,
            };
            SendMessage(message);
			tbSend.Text = "";

        }
        private void SendFileAction(object sender, RoutedEventArgs e)
        {
            btSend.Visibility = Visibility.Visible;
            btAddFile.Visibility = Visibility.Visible;

            btNegative.Visibility = Visibility.Hidden;
            btAccept.Visibility = Visibility.Hidden;

            Message message = new Message()
            {
                From = userLogin,
                To = curentUser,
                Command = CommandMessage.SendFile,
                TextMessage = string.Empty,
                FileName = fileName,
                FileData = GetFileContent(),
                CreatedAt = DateTime.Now,
            };
            fileName = string.Empty;
            pathToFile = string.Empty;
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
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (socket!=null)
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

        private void btSelectAction(object sender, RoutedEventArgs e)
        {
			if (curentUser == string.Empty)
			{
				return;
			}

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                pathToFile = ofd.FileName;
                fileName = ofd.SafeFileName;
                btSend.Visibility = Visibility.Hidden;
                btAddFile.Visibility = Visibility.Hidden;

                btNegative.Visibility = Visibility.Visible;
                btAccept.Visibility = Visibility.Visible;
             
            }
        }
        #endregion

        #region UI
        private void CleanTextBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Write message...")
            {
                (sender as TextBox).Text = "";
            }

        }
        private void AddTextBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "Write message...";
            }
        }
        private void DontTrandportFileAction(object sender, RoutedEventArgs e)
        {
            btSend.Visibility = Visibility.Visible;
            btAddFile.Visibility = Visibility.Visible;

            btNegative.Visibility = Visibility.Hidden;
            btAccept.Visibility = Visibility.Hidden;
        }
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
        private void MaouseEnterUI(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.AliceBlue;
        }

        private void MouseLeaveUI(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.Transparent;
        }

        private void CreatePlaceUI(List<HistoryMessages> history)
		{
			int indexfile = 0;
			curentFiles.Clear();

			Dispatcher.Invoke(() =>
			{
                canPlace.Children.Clear();
			});
			positionMessage = 10;

            foreach (HistoryMessages message in history)
			{

				Dispatcher.Invoke(() =>
                {
                    if (canPlace.Children.Count > 0)
                        positionMessage += 100;
						//positionMessage += (canPlace.Children[canPlace.Children.Count - 1] as Border).ActualHeight + 10;
                   
                    Border border = new Border() { MaxWidth = 200, Background = Brushes.LightBlue, Padding=new Thickness(10), CornerRadius=new CornerRadius(20)};

                    if (string.IsNullOrEmpty(message.Message)) {

						if(File.Exists(dirPath + message.FileName))
						{
							border.Child = CreateImage(message.FileName);
						}
						else
						{
                            curentFiles.Add(message);
                            Label messageLabel = new Label() { Content = message.FileName, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                            border.Child = messageLabel;
                            border.Tag = indexfile++;
                            border.MouseLeftButtonUp += DownloadAction;
                        }
						
					}
					else
					{
                        StackPanel stack = new StackPanel() { Margin=new Thickness(10) };
                        Label label = new Label() { Content = message.Message, FontSize = 16 };
                        Label labeldate = new Label() { Content = DateTime.Now.ToString(), FontSize =8 };

                        border.Child = stack;
                        stack.Children.Add(label);
                        stack.Children.Add(labeldate);
                    }

					Canvas.SetTop(border, positionMessage);					
					if(message.UserName == userLogin)
					{
						Canvas.SetRight(border, 10);
						canPlace.Children.Add(border);
                    }
					else
					{
                        Canvas.SetLeft(border, 10);
                        canPlace.Children.Add(border);
                    }
                });
				
			}
		}

        private void DownloadAction(object sender, MouseButtonEventArgs e)
        {

			HistoryMessages file = curentFiles[int.Parse((sender as Border).Tag.ToString())];
            if (file.bytes == null) return;
            using (FileStream fs = new FileStream(dirPath + file.FileName, FileMode.Create))
            {
                fs.Write(file.bytes, 0, file.bytes.Length);
            }

			 (sender as Border).Child = CreateImage(file.FileName);
        }

		private Image CreateImage(string name)
		{
            System.Windows.Controls.Image myImage = new Image();
            myImage.Width = 200;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(dirPath + name);
            myBitmapImage.EndInit();

            myImage.Source = myBitmapImage;
            return myImage;
        }

        private void EntryTextChange()
        {
            int index = 0;
            while (true)
            {
                if(index<3)
                    Dispatcher.Invoke(() => tbOpus.Text += ".");
                else
                {
                    Dispatcher.Invoke(() => tbOpus.Text = "Please wait for answwer for server");
                    index = -1;
                }
                   

                index++;
                System.Threading.Thread.Sleep(700);
            }
        }
        #endregion





        private byte[] GetFileContent()
		{
			try
			{
				if (string.IsNullOrEmpty(pathToFile)) return null;
				using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
				{
					BinaryReader reader = new BinaryReader(fs);
					byte[] buffer = reader.ReadBytes((int)reader.BaseStream.Length);
					reader.Close();
					return buffer;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("GetFileContent: " + ex.Message);
				return null;
			}

		}

        private void OpenDirectoryAction(object sender, MouseButtonEventArgs e)
        {
            //string open = dirPath;
           // open = open.Remove(open.Length - 1);

            //Process.Start("C:\\Hasiuk\\GitHub\\.Net\\LocalMessenger\\Client_LocalMessenger\\Client_LocalMessenger\\bin\\Debug\\net6.0-windows\\data");
        }

     
    }
}