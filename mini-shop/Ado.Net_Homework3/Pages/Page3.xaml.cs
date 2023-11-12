using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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

namespace Ado.Net_Homework3.Pages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }
        private void EnterWorkBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            p3listbox.Items.Clear();

            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();

                  
               
                    SqlCommand commandAllInfo = new SqlCommand("Select * from [Sklad];", con);
                    var readrer = commandAllInfo.ExecuteReader();

                    while (readrer.Read())
                    {
                        Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                        Grid grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());

                        TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<string>(1), Margin = new Thickness(5) };
                        TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<string>(2), Margin = new Thickness(5) };
                        TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<string>(3), Margin = new Thickness(5) };
                        TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                        TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                        TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

                        Grid.SetColumn(datetxt, 0);
                        grid.Children.Add(nametxt);
                        Grid.SetColumn(typetxt, 1);
                        grid.Children.Add(typetxt);
                        Grid.SetColumn(vendortxt, 2);
                        grid.Children.Add(vendortxt);
                        Grid.SetColumn(quantitytxt, 3);
                        grid.Children.Add(quantitytxt);
                        Grid.SetColumn(pricetxt, 4);
                        grid.Children.Add(pricetxt);
                        Grid.SetColumn(datetxt, 5);
                        grid.Children.Add(datetxt);


                        border.Child = grid;
                        p3listbox.Items.Add(border);

                        popup1.IsOpen = true;
                    }
                         
                       

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }


            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.White;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.Transparent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();

                    SqlCommand commandadd = new SqlCommand($"Insert [Sklad] values ('{nametb.Text}','{typetb.Text}','{vendortb.Text}',{Convert.ToInt32(quntb.Text)},{Convert.ToDecimal(pricetb.Text)},CURRENT_TIMESTAMP);", con);
                    commandadd.ExecuteNonQuery();
                    popup1.IsOpen = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally { con.Close(); }
            }
        }
    }
}
