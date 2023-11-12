using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Image = System.Windows.Controls.Image;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //для відкриття і зберігання
        private OpenFileDialog openfileDialog;//Відкриття файлу
        private SaveFileDialog savefileDialog;//Збереження файлу
        private PrintDialog printDialog;

        private readonly string  workfilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

        private bool isOpenPopup=false;
        private bool[] seeChange;

        //Для Вікон
        List<TextBox> texts;
        int openwind;
        bool allsize;
        private Point lastpoint;

        //Розмір шрифту
        int shruftsize;
        int mashtab;
        public MainWindow()
        {
            InitializeComponent();
            InitMyComp();
        }
        private void InitMyComp()
        {
            seeChange=new bool[2];
            for(int i=0;i<seeChange.Length;i++)
            {
                seeChange[i]=true;
            }
            texts=new List<TextBox>();
            openwind=0;

            openfileDialog = new OpenFileDialog();
            openfileDialog.Filter = workfilter;
            savefileDialog = new SaveFileDialog();
            savefileDialog.Filter = workfilter;
            printDialog=new PrintDialog();
            shruftsize = 14;
            mashtab = 100;
            allsize = false;
        }
      

        private void MenuChangeColor_Border_MouseEnter(object sender, MouseEventArgs e)
        { 
            //Зміна кольору у меню
            (sender as Border).Background =new SolidColorBrush((Color)ColorConverter.ConvertFromString("#272d38"));

            //Перевірка чи є ще відкритий popup 
            if (isOpenPopup)
            {
                FilePopup.IsOpen = false;
                EditPopup.IsOpen = false;
                SeePopup.IsOpen = false;

                if((sender as Border).Child is StackPanel)
                    (((sender as Border).Child as StackPanel).Children[1] as Popup).IsOpen = true;
              
            }
            
            
        }

        private void MenuChangeColor_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            //Зміна кольору у меню
            (sender as Border).Background = Brushes.Transparent;
        }

        private void ItemInMenuVangeBackground_Border_MouseEnter(object sender, MouseEventArgs e)
        {
            //Зміна елементів кольору у меню
            (sender as Border).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#383838"));
        }

        private void ItemInMenuVangeBackground_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            //Зміна елементів кольору у меню
            (sender as Border).Background = Brushes.Transparent;
        }

        private void CallPopup_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Відкриття popup
            if((sender as Border).Child is StackPanel)
            {
                switch ((((sender as Border).Child as StackPanel).Children[0] as TextBlock).Text)
                {
                    case "Файл":
                        FilePopup.IsOpen = true;
                        isOpenPopup = true;
                        break;
                    case "Редагувати":
                        EditPopup.IsOpen = true;
                        isOpenPopup = true;
                        break;
                    case "Переглянути":
                        SeePopup.IsOpen = true;
                        isOpenPopup = true;
                        break;
                }
            }
            else
            {
                (((sender as Border).Child as Grid).Children[2] as Popup).IsOpen = true;
                isOpenPopup = true;
            }
          
        }

        private void TextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Закривання popup
            FilePopup.IsOpen = false;
            EditPopup.IsOpen = false;
            SeePopup.IsOpen = false;
            isOpenPopup = false;
        }

        private void SeeChange_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Обробка на включенні елементи

            if((((sender as Border).Child as Grid).Children[1] as TextBlock).Text== "Рядок Стану")
            {
                seeChange[0] = !seeChange[0];
                if (seeChange[0])
                {
                    (((sender as Border).Child as Grid).Children[0] as Image).Visibility = Visibility.Visible;
                    BottomPanel.Visibility=Visibility.Visible;
                }
                else
                {
                    (((sender as Border).Child as Grid).Children[0] as Image).Visibility = Visibility.Hidden;
                    BottomPanel.Visibility = Visibility.Collapsed;
                }
               
            }
            if ((((sender as Border).Child as Grid).Children[1] as TextBlock).Text == "Перенос по словах")
            {
                seeChange[1] = !seeChange[1];
                if (!seeChange[1])
                {
                    (((sender as Border).Child as Grid).Children[0] as Image).Visibility = Visibility.Visible;
                    Notepad.TextWrapping=TextWrapping.Wrap;
                }
                else
                {
                    (((sender as Border).Child as Grid).Children[0] as Image).Visibility = Visibility.Hidden;
                    Notepad.TextWrapping = TextWrapping.NoWrap;
                }
            }

            
        }
//Вікна
        private void TestaddWindButton_Click(object sender, RoutedEventArgs e)
        {

            Border border = new Border() { Background = Brushes.Transparent, CornerRadius=new CornerRadius(10,10,0,0), Margin=new Thickness(3,5,5,0) };
            Grid grid = new Grid();
            border.Child = grid;
            border.MouseLeftButtonUp += ChangeWindow_MouseLeftButtonUp;

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) });

            TextBlock name= new TextBlock() { Margin = new Thickness(5,0,100,0), VerticalAlignment = VerticalAlignment.Center, Foreground=Brushes.White,Text="Новий файл" };

            Border bordertxt = new Border() { Margin =new Thickness(5) };
            TextBlock del = new TextBlock() { Margin = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Center,FontSize=10, Foreground = Brushes.White, Text = "X" };
            bordertxt.Child = del;
            bordertxt.MouseLeftButtonUp += CloseWindow_MouseLeftButtonUp;
            bordertxt.MouseEnter += MenuChangeColor_Border_MouseEnter;
            bordertxt.MouseLeave += MenuChangeColor_Border_MouseLeave;

            Grid.SetColumn(name, 0);
            grid.Children.Add(name);

            Grid.SetColumn(bordertxt, 1);
            grid.Children.Add(bordertxt);


            WindowPanel.Children.Add(border);
            texts.Add(new TextBox());
        }

        private void CloseWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Видалення вкладки
            
            Border wind = ((sender as Border).Parent as Grid).Parent as Border;
           
            if (WindowPanel.Children.IndexOf(wind) == openwind)
            {
                openwind = 0;
                (WindowPanel.Children[0] as Border).Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1a1f2d"));
                Notepad.Text = texts[0].Text;
            }

            texts.Remove(texts[WindowPanel.Children.IndexOf(wind)]);
            WindowPanel.Children.Remove(wind);
            e.Handled = true;

            if (WindowPanel.Children.Count == 0)
            {
                this.Close();
            }
        }
        private void ChangeWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //вибір вікна
            texts[openwind].Text = Notepad.Text;
         
            (sender as Border).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1a1f2d"));
            openwind= WindowPanel.Children.IndexOf(sender as Border);
            Notepad.Text = texts[openwind].Text;

            for (int i=0; i<WindowPanel.Children.Count; i++)
            {
                if (i != openwind)
                {
                    (WindowPanel.Children[i] as Border).Background = Brushes.Transparent;
                }
            }
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //перше вікно
            Border border = new Border() {  Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1a1f2d")), CornerRadius = new CornerRadius(10, 10, 0, 0), Margin = new Thickness(3, 5, 5, 0) };
            Grid grid = new Grid();
            border.Child = grid;
            border.MouseLeftButtonUp += ChangeWindow_MouseLeftButtonUp;

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) });

            TextBlock name = new TextBlock() { Margin = new Thickness(5, 0, 100, 0), VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.White, Text = "Новий файл" };

            Border bordertxt = new Border() { Margin = new Thickness(5) };
            TextBlock del = new TextBlock() { Margin = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Center, FontSize = 10, Foreground = Brushes.White, Text = "X" };
            bordertxt.Child = del;
            bordertxt.MouseLeftButtonUp += CloseWindow_MouseLeftButtonUp;
            bordertxt.MouseEnter += MenuChangeColor_Border_MouseEnter;
            bordertxt.MouseLeave += MenuChangeColor_Border_MouseLeave;

            Grid.SetColumn(name, 0);
            grid.Children.Add(name);

            Grid.SetColumn(bordertxt, 1);
            grid.Children.Add(bordertxt);


            WindowPanel.Children.Add(border);
            texts.Add(new TextBox());

            Notepad.FontSize = shruftsize;
        }

       

        private void Notepad_TextChanged(object sender, TextChangedEventArgs e)
        {
            int lineNumber = Notepad.GetLineIndexFromCharacterIndex(Notepad.CaretIndex);
            ryadok.Text = lineNumber.ToString()+",";
            int ryadnumber = Notepad.CaretIndex - Notepad.GetCharacterIndexFromLineIndex(lineNumber);
            stovb.Text=ryadnumber.ToString();
        }

        private void CreateWindow_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Border border = new Border() { Background = Brushes.Transparent, CornerRadius = new CornerRadius(10, 10, 0, 0), Margin = new Thickness(3, 5, 5, 0) };
            Grid grid = new Grid();
            border.Child = grid;
            border.MouseLeftButtonUp += ChangeWindow_MouseLeftButtonUp;

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) });

            TextBlock name = new TextBlock() { Margin = new Thickness(5, 0, 100, 0), VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.White, Text = "Новий файл" };

            Border bordertxt = new Border() { Margin = new Thickness(5) };
            TextBlock del = new TextBlock() { Margin = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Center, FontSize = 10, Foreground = Brushes.White, Text = "X" };
            bordertxt.Child = del;
            bordertxt.MouseLeftButtonUp += CloseWindow_MouseLeftButtonUp;
            bordertxt.MouseEnter += MenuChangeColor_Border_MouseEnter;
            bordertxt.MouseLeave += MenuChangeColor_Border_MouseLeave;

            Grid.SetColumn(name, 0);
            grid.Children.Add(name);

            Grid.SetColumn(bordertxt, 1);
            grid.Children.Add(bordertxt);


            WindowPanel.Children.Add(border);
            texts.Add(new TextBox());
            
        }

        private void Open_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (openfileDialog.ShowDialog() == true)
            {
                Notepad.Text = File.ReadAllText(openfileDialog.FileName);
                
            }
        }

        private void Save_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (savefileDialog.ShowDialog() == true)
            {
                File.WriteAllText(savefileDialog.FileName, Notepad.Text);
            }
        }

        private void SaveAs_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            if (savefileDialog.ShowDialog() == true)
            {
                File.WriteAllText(savefileDialog.FileName, Notepad.Text);
            }
        }
        private void Print_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           
            if (printDialog.ShowDialog() == true)
            {
              
            }
        }

        private void Filepopactiv_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //нажаття на клавіши
            TextBlock text = ((sender as Border).Child as Grid).Children[0] as TextBlock;
            FilePopup.IsOpen = false;

            if (text.Text == "Створити вкладку") {
                Border border = new Border() { Background = Brushes.Transparent, CornerRadius = new CornerRadius(10, 10, 0, 0), Margin = new Thickness(3, 5, 5, 0) };
                Grid grid = new Grid();
                border.Child = grid;
                border.MouseLeftButtonUp += ChangeWindow_MouseLeftButtonUp;

                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) });

                TextBlock name = new TextBlock() { Margin = new Thickness(5, 0, 100, 0), VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.White, Text = "Новий файл" };

                Border bordertxt = new Border() { Margin = new Thickness(5) };
                TextBlock del = new TextBlock() { Margin = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Center, FontSize = 10, Foreground = Brushes.White, Text = "X" };
                bordertxt.Child = del;
                bordertxt.MouseLeftButtonUp += CloseWindow_MouseLeftButtonUp;
                bordertxt.MouseEnter += MenuChangeColor_Border_MouseEnter;
                bordertxt.MouseLeave += MenuChangeColor_Border_MouseLeave;

                Grid.SetColumn(name, 0);
                grid.Children.Add(name);

                Grid.SetColumn(bordertxt, 1);
                grid.Children.Add(bordertxt);


                WindowPanel.Children.Add(border);
                texts.Add(new TextBox());
            }
            if(text.Text == "Відкрити")
            {
                if (openfileDialog.ShowDialog() == true)
                {
                    Notepad.Text = File.ReadAllText(openfileDialog.FileName);

                }
            }
            if(text.Text== "Зберегти")
            {
                if (savefileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(savefileDialog.FileName, Notepad.Text);
                }
            }
            if(text.Text== "Закрити вкладку")
            {
                Border wind = new Border();
                foreach (var item in WindowPanel.Children)
                {
                    if ((item as Border).Background != Brushes.Transparent)
                    {
                        wind = item as Border;
                    }
                }
               

                if (WindowPanel.Children.IndexOf(wind) == openwind)
                {
                    openwind = 0;
                    (WindowPanel.Children[0] as Border).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1a1f2d"));
                    Notepad.Text = texts[0].Text;
                }

                texts.Remove(texts[WindowPanel.Children.IndexOf(wind)]);
                WindowPanel.Children.Remove(wind);
                e.Handled = true;

                if (WindowPanel.Children.Count == 0)
                {
                    this.Close();
                }
            }
            if (text.Text == "Закрити вікно")
            {
                this.Close();
            }
            if (text.Text == "Вийти")
            {
                this.Close();
            }
            if (text.Text == "Друк")
            {
                if (printDialog.ShowDialog() == true)
                {

                }
            }


        }

        private void Redaguvatu_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock text = ((sender as Border).Child as Grid).Children[0] as TextBlock;
            EditPopup.IsOpen = false;

            if(text.Text=="Дата й час")
            {
                Notepad.Text += DateTime.Now.ToString();
            }
            if (text.Text == "Вибрати все")
            {
                Notepad.SelectAll();
            }
            if (text.Text == "Пошук")
            {
                poshukpopup.IsOpen = true;
               
            }
            if (text.Text == "Видалити")
            {
                Notepad.Text = Notepad.Text.Remove(Notepad.SelectionStart,Notepad.SelectionLength);
            }




        }

        private void Mashtab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock text = ((sender as Border).Child as Grid).Children[0] as TextBlock;

            if (text.Text == "Збільшити")
            {
                mashtab += 10;
                Mashtabtext.Text = mashtab.ToString()+"%";
                Notepad.FontSize = ++shruftsize;
            }
            if (text.Text == "Зменшити")
            {
                mashtab -= 10;
                Mashtabtext.Text = mashtab.ToString() + "%";
                Notepad.FontSize = --shruftsize;
            }
            if (text.Text == "Відновити масштаб за замовчуванням")
            {
                mashtab = 100;
                Mashtabtext.Text = mashtab.ToString() + "%";
                shruftsize = 14;
                Notepad.FontSize =shruftsize;
            }
           
        }

        private void Main_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = ((sender as Border).Child as Image);

            if (image.Name == "closewind")
            {
                this.Close();
            }
            if (image.Name == "max_normwind")
            {
                allsize = !allsize;

                if (allsize)
                    this.WindowState = WindowState.Maximized;
                else 
                    this.WindowState = WindowState.Normal;
            }
            if (image.Name == "minwind")
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void Notepad_MouseEnter(object sender, MouseEventArgs e)
        {
            FilePopup.IsOpen = false;
            EditPopup.IsOpen = false;
            SeePopup.IsOpen = false;
            isOpenPopup = false;
        }

        private void closefindBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            poshukpopup.IsOpen = false;
        }

        private void find_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string text = Notepad.Text;
            string word = poshuktextbox.Text;
            int index = text.IndexOf(word);
            if (index != -1)
            {
                Notepad.Select(index, word.Length);
            }
            poshukpopup.IsOpen = false;
        }

  

        private void MovewindowBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(this);
                lastpoint = new Point(point.X, point.Y);
            }

        }

        private void MoveWindowBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(this);
                this.Top += point.Y - lastpoint.Y;
                this.Left += point.X - lastpoint.X;
            }
        }

        private void selectallCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Notepad.SelectAll();
        }

        private void findCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            poshukpopup.IsOpen = true; 
        }
    }
}
