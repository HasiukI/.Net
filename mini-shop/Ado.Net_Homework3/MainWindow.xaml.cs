using Ado.Net_Homework3.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Ado.Net_Homework3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Page1 page1;
        Page2 page2;
        Page3 page3;
        Page4 page4;
        Page5 page5;
        public MainWindow()
        {
            InitializeComponent();
            InititalizePage();
        }
        
        private void InititalizePage()
        {
            page1 = new Page1();
            page2 = new Page2();    
            page3 = new Page3();    
            page4 = new Page4();
            page5 = new Page5();
            MainFrame.Content = page1;
        }

        private void EnterDateBase_Button_Click(object sender, RoutedEventArgs e)
        {
            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr)) {
               try 
                { 
                    con.Open();
                    MessageBox.Show("Ok");
                }
                catch(Exception ex) 
                {
                 MessageBox.Show(ex.Message);   
                }    
                finally { con.Close(); } 
            
            }
        }

        private void Decorate_Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.LightBlue;
        }

        private void Decorate_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.Transparent;
        }

        private void ChangeFrame_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           switch (((sender as Border).Child as TextBlock).Text)
            {
                case "1":
                    MainFrame.Content = page1;
                    break;
                case "2":
                    MainFrame.Content = page2;
                    break;
                case "3":
                    MainFrame.Content = page3;
                    break;
                case "4":
                    MainFrame.Content = page4;
                    break;
                case "5":
                    MainFrame.Content = page5;

                    break;

            }
        }
    }
}
