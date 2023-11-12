using Ado.Net_Homework3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void EnterWorkBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            p1listbox.Items.Clear();

            var constr = ConfigurationManager.ConnectionStrings["Sklad"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();

                    switch(((sender as Border).Child as TextBlock).Text)
                    {
                        case "Відображення всієї інформації про товар":
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
                                p1listbox.Items.Add(border);
                            }
                            break;
                        case "Відображення всіх типів товарів":

                            SqlCommand commandAllType = new SqlCommand("Select Distinct [Sklad].[Type] from[Sklad];", con);
                            var readrer2 = commandAllType.ExecuteReader();

                            while (readrer2.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = readrer2.GetString(0), Margin = new Thickness(5) };
                                border.Child= nametxt;
                                p1listbox.Items.Add(border);
                            }
                           
                            break;
                        case "Відображення всіх постачальників":

                            SqlCommand commandAllVendor = new SqlCommand("Select Distinct[Sklad].Vendor from[Sklad];", con);
                            var reader3 = commandAllVendor.ExecuteReader();

                            while (reader3.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader3.GetString(0), Margin = new Thickness(5) };
                                border.Child = nametxt;
                                p1listbox.Items.Add(border);
                            }
                            break;
                        case "Показати товар із максимальною кількістю":
                            SqlCommand commandmaxQuantity = new SqlCommand("Select * from [Sklad]\r\nwhere [Sklad].[Quantity]= (Select Max([Sklad].[Quantity]) from [Sklad]);", con);
                            var reader4 = commandmaxQuantity.ExecuteReader();

                            while (reader4.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                Grid grid = new Grid();
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());

                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader4.GetFieldValue<string>(1), Margin = new Thickness(5) };
                                TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader4.GetFieldValue<string>(2), Margin = new Thickness(5) };
                                TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader4.GetFieldValue<string>(3), Margin = new Thickness(5) };
                                TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader4.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                                TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader4.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                                TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader4.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                                p1listbox.Items.Add(border);
                            }
                            break;
                        case "Показати товар із мінімальною кількістю":
                            SqlCommand commandminQuantity = new SqlCommand("Select * from [Sklad]\r\nwhere [Sklad].[Quantity]= (Select Min([Sklad].[Quantity]) from [Sklad]);", con);
                            var reader5 = commandminQuantity.ExecuteReader();

                            while (reader5.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                Grid grid = new Grid();
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());

                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader5.GetFieldValue<string>(1), Margin = new Thickness(5) };
                                TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader5.GetFieldValue<string>(2), Margin = new Thickness(5) };
                                TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader5.GetFieldValue<string>(3), Margin = new Thickness(5) };
                                TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader5.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                                TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader5.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                                TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader5.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                                p1listbox.Items.Add(border);
                            }
                            break;
                        case "Показати товар із мінімальною собівартістю":
                            SqlCommand commandminPrice = new SqlCommand("Select * from [Sklad]\r\nwhere [Sklad].[Price]= (Select Min([Sklad].[Price]) from [Sklad]);", con);
                            var reader6 = commandminPrice.ExecuteReader();

                            while (reader6.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                Grid grid = new Grid();
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());

                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader6.GetFieldValue<string>(1), Margin = new Thickness(5) };
                                TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader6.GetFieldValue<string>(2), Margin = new Thickness(5) };
                                TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader6.GetFieldValue<string>(3), Margin = new Thickness(5) };
                                TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader6.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                                TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader6.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                                TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader6.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                                p1listbox.Items.Add(border);
                            }
                            break;
                        case "Показати товар із максимальною собівартістю":
                            SqlCommand commandmaxPrice = new SqlCommand("Select * from [Sklad]\r\nwhere [Sklad].[Price]= (Select Max([Sklad].[Price]) from [Sklad]);", con);
                            var reader7 = commandmaxPrice.ExecuteReader();

                            while (reader7.Read())
                            {
                                Border border = new Border() { Background = Brushes.DarkGray, CornerRadius = new CornerRadius(10) };
                                Grid grid = new Grid();
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());
                                grid.ColumnDefinitions.Add(new ColumnDefinition());

                                TextBlock nametxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader7.GetFieldValue<string>(1), Margin = new Thickness(5) };
                                TextBlock typetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader7.GetFieldValue<string>(2), Margin = new Thickness(5) };
                                TextBlock vendortxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader7.GetFieldValue<string>(3), Margin = new Thickness(5) };
                                TextBlock quantitytxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader7.GetFieldValue<int>(4).ToString(), Margin = new Thickness(5) };
                                TextBlock pricetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader7.GetFieldValue<decimal>(5).ToString(), Margin = new Thickness(10) };
                                TextBlock datetxt = new TextBlock() { FontSize = 15, Foreground = Brushes.White, FontWeight = FontWeights.Bold, Text = reader7.GetFieldValue<DateTime>(6).ToString(), Margin = new Thickness(50, 50, 10, 10) };

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
                                p1listbox.Items.Add(border);
                            }
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
