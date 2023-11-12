using Bll;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AirHokeyExam
{
    /// <summary>
    /// Interaction logic for StartGame.xaml
    /// </summary>
    public partial class StartGame : Window
    {
        AirLogic game;
        DispatcherTimer timer_punch;
        DispatcherTimer time_obert;
        DispatcherTimer end_game;
        DispatcherTimer timer;
        Winner winner;

        string[] phone = { "image/pole1.png", "image/pole2.png", "image/pole3.png" };
        string[] hand = { "image/hand1.png", "image/hand2.png", "image/hand3.png" };
        string[] sha = { "image/shaiba1.png", "image/shaiba2.png", "image/shaiba3.png" };

        bool[] PressKey_Player1 = new bool[4];
        bool[] PressKey_Player2 = new bool[4];


        public StartGame()
        {
            InitializeComponent();
        }

        public void SetSetting(int back, int _pl1, int _pl2, int _sh)
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(phone[back], UriKind.Relative));
            pole.Background = ib;

            ImageBrush pl1 = new ImageBrush();
            pl1.ImageSource = new BitmapImage(new Uri(hand[_pl1], UriKind.Relative));
            Player1.Fill = pl1;
            ImageBrush pl2 = new ImageBrush();
            pl2.ImageSource = new BitmapImage(new Uri(hand[_pl2], UriKind.Relative));
            Player2.Fill = pl2;
            ImageBrush sh = new ImageBrush();
            sh.ImageSource = new BitmapImage(new Uri(sha[_sh], UriKind.Relative));
            shaiba.Fill = sh;

        }
        private void Control_Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (Keyboard.IsKeyDown(Key.D))
                PressKey_Player1[0] = true;
            if (Keyboard.IsKeyDown(Key.A))
                PressKey_Player1[1] = true;
            if (Keyboard.IsKeyDown(Key.S))
                PressKey_Player1[2] = true;
            if (Keyboard.IsKeyDown(Key.W))
                PressKey_Player1[3] = true;

            if (Keyboard.IsKeyDown(Key.Left))
                PressKey_Player2[0] = true;
            if (Keyboard.IsKeyDown(Key.Right))
                PressKey_Player2[1] = true;
            if (Keyboard.IsKeyDown(Key.Up))
                PressKey_Player2[2] = true;
            if (Keyboard.IsKeyDown(Key.Down))
                PressKey_Player2[3] = true;





        }
        private void KeyboardControl_Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyUp(Key.D))
                PressKey_Player1[0] = false;
            if (Keyboard.IsKeyUp(Key.A))
                PressKey_Player1[1] = false;
            if (Keyboard.IsKeyUp(Key.S))
                PressKey_Player1[2] = false;
            if (Keyboard.IsKeyUp(Key.W))
                PressKey_Player1[3] = false;


            if (Keyboard.IsKeyUp(Key.Left))
                PressKey_Player2[0] = false;
            if (Keyboard.IsKeyUp(Key.Right))
                PressKey_Player2[1] = false;
            if (Keyboard.IsKeyUp(Key.Up))
                PressKey_Player2[2] = false;
            if (Keyboard.IsKeyUp(Key.Down))
                PressKey_Player2[3] = false;

        }
        private void TapBT(object sender, EventArgs e)
        {
            if (PressKey_Player1[0])
            {
                game.PressKeyDown("D");
            }

            if (PressKey_Player1[1])
            {
                game.PressKeyDown("A");
            }

            if (PressKey_Player1[2])
            {
                game.PressKeyDown("S");
            }

            if (PressKey_Player1[3])
            {
                game.PressKeyDown("W");
            }


            if (PressKey_Player2[0])
            {
                game.PressKeyDown("Left");
            }

            if (PressKey_Player2[1])
            {
                game.PressKeyDown("Right");
            }

            if (PressKey_Player2[2])
            {
                game.PressKeyDown("Up");
            }

            if (PressKey_Player2[3])
            {
                game.PressKeyDown("Down");
            }

            if (CheckColiziumForPlayer1())
                game.Improve(1, PressKey_Player1);
            if (CheckColiziumForPlayer2())
                game.Improve(2, PressKey_Player2);

        }
        private bool CheckColiziumForPlayer1()
        {
            Rect player = new Rect(game.Player1_x, game.Player1_y, 50, 50);
            Rect[] side = new Rect[4]{
                new Rect(0, 0, pole.ActualWidth / 2, 0), //top
                new Rect(0, pole.ActualHeight, pole.ActualWidth / 2, 0),//bootom
                new Rect(0, 0, 0, pole.ActualHeight), // left
                new Rect(pole.ActualWidth/2, 0,0,pole.ActualHeight) //right
            };

            for (int i = 0; i < 4; i++)
            {
                if (player.IntersectsWith(side[i]))
                {
                    return true;
                }
            }

            return false;

        }
        private bool CheckColiziumForPlayer2()
        {
            Rect player = new Rect(game.Player2_x, game.Player2_y, 50, 50);
            Rect[] side = new Rect[4]{
                new Rect(pole.ActualWidth / 2, 0, pole.ActualWidth / 2, 0), //top
                new Rect(pole.ActualWidth / 2, pole.ActualHeight, pole.ActualWidth / 2, 0),//bootom
                new Rect(pole.ActualWidth / 2, 0, 0, pole.ActualHeight), // left
                new Rect(pole.ActualWidth , 0,0,pole.ActualHeight) //right
            };

            for (int i = 0; i < 4; i++)
            {
                if (player.IntersectsWith(side[i]))
                {
                    return true;
                }
            }

            return false;

        }


        private void StartGame_Window_Loaded(object sender, RoutedEventArgs e)
        {
            game = new AirLogic(pole.ActualHeight, pole.ActualWidth);
            Canvas.SetLeft(Player1, game.Player1_x);
            Canvas.SetTop(Player1, game.Player1_y);

            Canvas.SetLeft(Player2, game.Player2_x);
            Canvas.SetTop(Player2, game.Player2_y);

            Canvas.SetLeft(shaiba, game.Shaiba_x);
            Canvas.SetTop(shaiba, game.Shaiba_y);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Update;
            timer.Tick += TapBT;
            timer.Start();

            timer_punch = new DispatcherTimer();
            timer_punch.Interval = TimeSpan.FromMilliseconds(10);
            timer_punch.Tick += UpdateSpeedShaiba;

            time_obert = new DispatcherTimer();
            time_obert.Interval = TimeSpan.FromMilliseconds(16);
            time_obert.Tick += Time_obert_Tick;

            end_game = new DispatcherTimer();
            end_game.Interval = TimeSpan.FromSeconds(1);
            end_game.Tick += End_game_Tick;
            end_game.Start();

            winner = new Winner();
        }

        private void End_game_Tick(object sender, EventArgs e)
        {
            --game.time_game;
            timer_to_end_txt.Text = game.time_game.ToString();
        }

        private void Update(object sender, EventArgs e)
        {
            if (GetWinner())
            {
                time_obert.Stop();
                time_obert = null;
                timer_punch.Stop();
                timer_punch = null;
                timer.Stop();
                timer = null;

                this.Close();
            }

            if (game.Time_Touch > 500)
            {
                timer_punch.Stop();
                game.Time_Touch = 0;
                time_obert.Stop();
            }

            Rect player1_rect = new Rect(game.Player1_x, game.Player1_y, 50, 50);
            Rect player2_rect = new Rect(game.Player2_x, game.Player2_y, 50, 50);
            Rect shaiba_rect = new Rect(game.Shaiba_x, game.Shaiba_y, 30, 30);

            Rect[] side = new Rect[6]{
                new Rect(0, 0, pole.ActualWidth, 0), //top
                new Rect(0, pole.ActualHeight, pole.ActualWidth, 0),//bootom
                new Rect(0, 0, 0, pole.ActualHeight/3), // left_TOP
                new Rect(0, pole.ActualHeight-pole.ActualHeight/3, 0, pole.ActualHeight/3),//left_bot
                new Rect(pole.ActualWidth, 0, 0, pole.ActualHeight/3), //right_top
                new Rect(pole.ActualWidth,pole.ActualHeight-pole.ActualHeight/3, 0, pole.ActualHeight/3) //right_bot
            };

            if (player1_rect.IntersectsWith(shaiba_rect))
            {
                
                game.TouchShaiba(1);
                timer_punch.Start();
                
                time_obert.Start();
               
            }
            if (player2_rect.IntersectsWith(shaiba_rect))
            {
              
                game.TouchShaiba(2);
                timer_punch.Start();
                time_obert.Start();
            }

            if (shaiba_rect.IntersectsWith(side[0]) || shaiba_rect.IntersectsWith(side[1]))
                game.Punch_y = -game.Punch_y;
            if (shaiba_rect.IntersectsWith(side[2]) || shaiba_rect.IntersectsWith(side[3]) || shaiba_rect.IntersectsWith(side[4]) || shaiba_rect.IntersectsWith(side[5]))
                game.Punch_x = -game.Punch_x;

            if (Canvas.GetLeft(shaiba) < -10)
            {
                game.Goal(1);
                Player2txt.Text = game.getScope(1).ToString();
                
            }
            
         
            if (Canvas.GetLeft(shaiba) > 800)
            {
                game.Goal(0);
                Player1txt.Text = game.getScope(0).ToString();
            }


            Canvas.SetLeft(shaiba, game.Shaiba_x);
            Canvas.SetTop(shaiba, game.Shaiba_y);
            Canvas.SetLeft(Player1, game.Player1_x);
            Canvas.SetTop(Player1, game.Player1_y);
            Canvas.SetLeft(Player2, game.Player2_x);
            Canvas.SetTop(Player2, game.Player2_y);
        }

        private bool GetWinner()
        {
            if (game.time_game == 0)
            {
                int pl1 = Convert.ToInt32((Player1txt.Text));
                int pl2 = Convert.ToInt32((Player2txt.Text));

                if (pl1 == pl2)
                {
                    
                    winner.setWinner(0);
                    winner.Show();
                    return true;

                }
                else if (pl1 > pl2)
                {
                
                    winner.setWinner(1);
                    winner.Show();
                    return true;
                }
                else
                {
            
                    winner.setWinner(2);
                    winner.Show();
                    return true;
                }



            }
            if (Player1txt.Text == "5")
            {
              
                winner.setWinner(1);
                winner.ShowDialog();
                return true;

            }
            if (Player2txt.Text == "5")
            {
              
                winner.setWinner(2);
                winner.Show();
                return true;
            }

            

            return false;
        }
        private void Time_obert_Tick(object sender, EventArgs e)
        {

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = 20;
            rotateTransform.CenterY = 20;
            shaiba.RenderTransform = rotateTransform;
            rotateTransform.Angle=game.rotate();
        }

        private void UpdateSpeedShaiba(object sender, EventArgs e)
        {
            game.SlowlySpeed();
            game.Shaiba_x += game.Punch_x;
            game.Shaiba_y += game.Punch_y;
        }

        private void CloseGame_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.LightBlue;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.Transparent;
        }
    }
}

