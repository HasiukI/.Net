using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Sys_homework_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void ProccesStart_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch((sender as Border).Name)
            {
                case "paint_border":
                        Process.Start("mspaint.exe");
                    break;
                case "block_border":
                        Process.Start("Notepad.exe");
                    break;
                case "calc_border":
                        Process.Start("calc.exe");
                    break;
            }
        }

        private void DispatcherBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DispatherWindow dispather = new DispatherWindow();
            dispather.ShowDialog();
        }

        private void calc_border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as StackPanel).Background = Brushes.Gray;
            (sender as StackPanel).OpacityMask = Brushes.Gray; 
           
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as StackPanel).Background = Brushes.Transparent;

        }
    }
}
