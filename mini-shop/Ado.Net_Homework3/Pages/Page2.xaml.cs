using Ado.Net_Homework3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {

        public Page2()
        {
            InitializeComponent();
        }

        private void EnterWorkBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();

                   switch(((sender as Border).Child as TextBlock).Text)
                    {
                        case "Показати товари, заданої категорії":
                            (popup1.Child as StackPanel).Children.Clear();
                            p2listbox.Items.Clear();

                           SqlCommand commandAllType = new SqlCommand("Select Distinct [Sklad].[Type] from[Sklad];", con);
                            var readrer2 = commandAllType.ExecuteReader();

                            while (readrer2.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10), Margin = new Thickness(5), BorderThickness = new Thickness(3) };
                                border.MouseLeftButtonUp += TypeBorder_MouseLeftButtonUp;
                                border.MouseEnter += Border_MouseEnter;
                                border.MouseLeave += Border_MouseLeave;
                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer2.GetString(0), Margin = new Thickness(5) };
                                border.Child = nametxt;
                                (popup1.Child as StackPanel).Children.Add(border);
                            }
                            popup1.IsOpen = true;
                        
                            break;
                        case "Показати товари, задані постачальником":
                            (popup2.Child as StackPanel).Children.Clear();
                            p2listbox.Items.Clear();

                            SqlCommand commandType = new SqlCommand("Select Distinct [Sklad].[Vendor] from[Sklad];", con);
                            var readrer3 = commandType.ExecuteReader();

                            while (readrer3.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10), Margin = new Thickness(5), BorderThickness = new Thickness(3) };
                                border.MouseLeftButtonUp += vENDORBorder_MouseLeftButtonUp;
                                border.MouseEnter += Border_MouseEnter;
                                border.MouseLeave += Border_MouseLeave;
                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetString(0), Margin = new Thickness(5) };
                                border.Child = nametxt;
                                (popup2.Child as StackPanel).Children.Add(border);
                            }
                            popup2.IsOpen = true;
                            break;
                        case "Показати найстаріший товар на складі":
                            SqlCommand commandOLD = new SqlCommand("Select * from [Sklad]\r\nwhere [Sklad].[Date]= (Select MAx([Date]) from [Sklad]);", con);
                            var readrer4 = commandOLD.ExecuteReader();
                            while (readrer4.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                Grid grid = new Grid();
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());

                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer4.GetFieldValue<string>(1), Margin = new Thickness(5) };
                                TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer4.GetFieldValue<string>(2), Margin = new Thickness(5) };
                                TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer4.GetFieldValue<string>(3), Margin = new Thickness(5) };
                                TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer4.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                                TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer4.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                                TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer4.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                                p2listbox.Items.Add(border);
                            }
                            break;                           
                        case "Показати середню кількість товарів по кожному типу товару":
                            // SqlCommand commandAVg = new SqlCommand("Select AVG([Sklad].Quantity) from[Sklad];", con);
                            //var readrer5 = commandAVg.ExecuteReader();
                            //while (readrer5.Read())
                            //{
                            //    Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                            //    Grid grid = new Grid();
                            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
                            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
                            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
                            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
                            //    grid.ColumnDefinitions.Add(new ColumnDefinition());

                            //    TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer5.GetFieldValue<string>(1), Margin = new Thickness(5) };
                            //    TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer5.GetFieldValue<string>(2), Margin = new Thickness(5) };
                            //    TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer5.GetFieldValue<string>(3), Margin = new Thickness(5) };
                            //    TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer5.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                            //    TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer5.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                            //    TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer5.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

                            //    Grid.SetColumn(datetxt, 0);
                            //    grid.Children.Add(nametxt);
                            //    Grid.SetColumn(typetxt, 1);
                            //    grid.Children.Add(typetxt);
                            //    Grid.SetColumn(vendortxt, 2);
                            //    grid.Children.Add(vendortxt);
                            //    Grid.SetColumn(quantitytxt, 3);
                            //    grid.Children.Add(quantitytxt);
                            //    Grid.SetColumn(pricetxt, 4);
                            //    grid.Children.Add(pricetxt);
                            //    Grid.SetColumn(datetxt, 5);
                            //    grid.Children.Add(datetxt);


                            //    border.Child = grid;
                            //    p2listbox.Items.Add(border);
                            //}
                            break;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }

            }

        }

        private void vENDORBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popup2.IsOpen = false;


            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {

                try
                {
                    con.Open();
                    SqlCommand commandAllType = new SqlCommand($"Exec FindVendor '{((sender as Border).Child as TextBlock).Text}';", con);
                    var readrer3 = commandAllType.ExecuteReader();
                    while (readrer3.Read())
                    {
                        Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                        Grid grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());

                        TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<string>(1), Margin = new Thickness(5) };
                        TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<string>(2), Margin = new Thickness(5) };
                        TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<string>(3), Margin = new Thickness(5) };
                        TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                        TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                        TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                        p2listbox.Items.Add(border);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }

            }
        }

        private void TypeBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            popup1.IsOpen = false;
            

            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
            
                try
                {
                    con.Open();
                    SqlCommand commandAllType = new SqlCommand($"Exec FindType '{((sender as Border).Child as TextBlock).Text}';", con);
                    var readrer3 = commandAllType.ExecuteReader();
                    while (readrer3.Read())
                    {
                        Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                        Grid grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.ColumnDefinitions.Add(new ColumnDefinition());

                        TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<string>(1), Margin = new Thickness(5) };
                        TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<string>(2), Margin = new Thickness(5) };
                        TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<string>(3), Margin = new Thickness(5) };
                        TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                        TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                        TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer3.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                        p2listbox.Items.Add(border);
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
    }
}
