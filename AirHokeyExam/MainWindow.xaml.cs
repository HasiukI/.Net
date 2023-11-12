using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AirHokeyExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool issound = true;
        bool hand = true;
        int hand1 = 0, hand2 = 1;
        int back = 0;
        int shaib = 0;
        public MainWindow()
        {
            InitializeComponent();
           
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //menuMusic.Volume = 50;
            //menuMusic.Play();
           

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            settingPopup.IsOpen = true;
        }

        private void IsSoundPlay_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            BitmapImage bi = new BitmapImage();
            string[] mus = { "image/soundon.png", "image/soundoff.png" };

            issound = !issound;

            if (issound)
            {
                bi.BeginInit();
                bi.UriSource = new Uri(mus[0], UriKind.Relative);
                bi.EndInit();
                soundimage.Source = bi;
                //menuMusic.Play();
            }
            else
            {

                bi.BeginInit();
                bi.UriSource = new Uri(mus[1], UriKind.Relative);
                bi.EndInit();
                soundimage.Source = bi;
                //menuMusic.Stop();
            }
        }

        private void CloseSetting_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            settingPopup.IsOpen=false;
        }

        private void Start_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StartGame game = new StartGame();
            game.SetSetting(back,hand1,hand2,shaib);
            game.ShowDialog();
        }

        private void CheckPole_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            b0.Background = Brushes.Transparent;
            b1.Background = Brushes.Transparent;
            b2.Background = Brushes.Transparent;

            (sender as Border).Background = Brushes.LightBlue;
            back = Convert.ToInt32((sender as Border).Name[1]-'0');
        }

        private void ChangeHand_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            hand = !hand;

            if (hand)
            {
                ((player1 as Image).Parent as Border).Background = Brushes.LightBlue;
                ((player2 as Image).Parent as Border).Background = Brushes.Transparent;
            }
            else
            {
                ((player2 as Image).Parent as Border).Background = Brushes.LightBlue;
                ((player1 as Image).Parent as Border).Background = Brushes.Transparent;
            }
        }

        private void Change_Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (hand)
            {
                player1.Source = (sender as Image).Source;
                hand1 = Convert.ToInt32((sender as Image).Name[1] - '0');
            }
            else
            {
                player2.Source = (sender as Image).Source;
                hand2 = Convert.ToInt32((sender as Image).Name[1] - '0');
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.LightBlue;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.Transparent;
        }

        private void shaibaCheck_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sh0.Background = Brushes.Transparent;
            sh1.Background = Brushes.Transparent;
            sh2.Background = Brushes.Transparent;

            (sender as Border).Background = Brushes.LightBlue;
            shaib = Convert.ToInt32((sender as Border).Name[2] - '0');
        }
    }
}
