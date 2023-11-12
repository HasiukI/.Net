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

namespace Ado.Net_Homework3.Pages
{
    /// <summary>
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();

        }

        private void EnterWorkBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            p4listbox.Items.Clear();

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
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());

                        TextBlock idtxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<int>(0).ToString(), Margin = new Thickness(5) };
                        TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<string>(1), Margin = new Thickness(5) };
                        TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<string>(2), Margin = new Thickness(5) };
                        TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<string>(3), Margin = new Thickness(5) };
                        TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                        TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                        TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };
                        Button button = new Button() { Margin = new Thickness(30), Content = "Видалити" };

                        button.Click += deleteButton_Click;

                        Grid.SetColumn(idtxt, 0);
                        grid.Children.Add(idtxt);
                        Grid.SetColumn(nametxt, 1);
                        grid.Children.Add(nametxt);
                        Grid.SetColumn(typetxt, 2);
                        grid.Children.Add(typetxt);
                        Grid.SetColumn(vendortxt, 3);
                        grid.Children.Add(vendortxt);
                        Grid.SetColumn(quantitytxt, 4);
                        grid.Children.Add(quantitytxt);
                        Grid.SetColumn(pricetxt, 5);
                        grid.Children.Add(pricetxt);
                        Grid.SetColumn(datetxt, 6);
                        grid.Children.Add(datetxt);
                        Grid.SetColumn(button, 7);
                        grid.Children.Add(button);

                        border.Child = grid;
                        p4listbox.Items.Add(border);
                    }
                          
                      
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }

            }

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            p4listbox.Items.Clear();

            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();

                    SqlCommand commandAllInfo = new SqlCommand($"exec DeleteCol {(((sender as Button).Parent as Grid).Children[0] as TextBlock).Text}", con);
                    commandAllInfo.ExecuteNonQuery();
                   
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
    } 
}

