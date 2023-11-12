using BLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Exam_Sys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileWork read_write;
        Thread thread_file, thread_game;
        Ellipse el;
        Image imagecar;

        int rez = 0;
        int pos =0;

        public MainWindow()
        {
            InitializeComponent();
            el = new Ellipse() { Width = 10, Height = 10, Fill = Brushes.White };
            el.MouseLeftButtonUp += El_MouseLeftButtonUp;
            read_write = new FileWork();
            GamePause.Children.Add(el);

            imagecar = new Image() {  Height = 40};
            imagecar.Source = new BitmapImage(new Uri(read_write.getPhoto()));
            Canvas.SetBottom(imagecar, 0);
            Canvas.SetLeft(imagecar, 0);
            Progress.Children.Add(imagecar);
        }
        #region FileWorks
        async private void addWordMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!String.IsNullOrEmpty(Word_TBox.Text) && !String.IsNullOrWhiteSpace(Word_TBox.Text))
            {
                if (Word_TBox.Text.Length < 20 && Word_TBox.Text.Length >= 3)
                {
                    if (Word_TBox.Text.IndexOfAny(new char[] { ' ', '.', '/', '*', ',', '[', ']', '<', '>', '(', ')', ':', ';', '?', '!', '}', '{' }) == -1)
                    {
                        await read_write.AddWordAsync(Word_TBox.Text);
                        WordsInFile_LB.ItemsSource = await read_write.AsyncReadFileWord();
                        Word_TBox.Text = "";
                    }
                }
            }
        }
        private void WordsInFile_LB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectedWords_LB.Items.IndexOf(WordsInFile_LB.SelectedItem) == -1)
            {
                SelectedWords_LB.Items.Add(WordsInFile_LB.SelectedItem);
            }
        }

        private void SelectedWords_LB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectedWords_LB.Items.Remove(SelectedWords_LB.SelectedItem);
        }

        private void OpenFileMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Border).Name == "find_bord")
                read_write.OpenFile("FindFiles");
            if ((sender as Border).Name == "words_bord")
                read_write.OpenFile("Words");

        }
        #endregion
        #region Window
        async private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WordsInFile_LB.ItemsSource = await read_write.AsyncReadFileWord();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(thread_game!=null)
                thread_game.Abort();
            if (thread_file != null)
                thread_file.Abort();
        }
        #endregion
        #region GameWork
        private void updateGame()
        {
            Stopwatch timer_spawn = new Stopwatch();
            timer_spawn.Start();
            while (true)
            {

                if (timer_spawn.Elapsed.TotalSeconds > 1)
                {
                    Task.Run(() => createElipse(Randomizer.GetPos(Convert.ToInt32(GamePause.ActualHeight), Convert.ToInt32(GamePause.ActualWidth))));
                    timer_spawn.Restart();
                }

            }
        }
        private void createElipse(int[] pos)
        {

            Dispatcher.Invoke(() => Canvas.SetLeft(el, pos[0]));
            Dispatcher.Invoke(() => Canvas.SetTop(el, pos[1]));

        }

        private void El_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rez++;
            rez_tb.Text = $"Ваші бали: {rez}";
        }
        private void RunCur()
        {
            int run = 0;
            Dispatcher.Invoke(() => run = Convert.ToInt32(Progress.ActualWidth) * read_write.Progress / read_write.MaxProgress);
            Dispatcher.Invoke(() => Canvas.SetLeft(imagecar, run));
        }
        #endregion


        async private void FindStart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<string> words = new List<string>();
            thread_file = new Thread(UpdateValue);
            thread_file.Start();
            thread_game = new Thread(updateGame);
            thread_game.Start();
            foreach (string word in SelectedWords_LB.Items)
            {
                words.Add(word);
            }

            await read_write.FindWords(words);
            
        }

        #region Decor
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.LightGray;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.White;
        }

        #endregion

      

        private void UpdateValue()
        {
            while (true)
            {
                if (Dispatcher.Invoke(() => read_write.Progress) >= Dispatcher.Invoke(() => read_write.MaxProgress)-20)
                {
                    thread_file.Abort();
                    thread_game.Abort();
                }
                Task.Run(() => RunCur());
            }

        }
       
       
       

       
    }
    static class Randomizer
    {
        static private Random random = new Random();
        public static double GetTimeSpawn()
        {
            return random.Next(0, 3);
        }
        public static int[] GetPos(int h, int w)
        {
            return new int[] { random.Next(0, h ), random.Next(0, w) };
        }
    }
}
