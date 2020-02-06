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
using System.Net;

namespace Print_Tower
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            MainWindow.dt.Stop();

            this.IP_Adress_Box.Text = DBConnect.server;
            this.DBname_Box.Text = DBConnect.database;
            this.Username_Box.Text = DBConnect.uid;
            this.Password_Box.Text = DBConnect.password;
            this.TableName_Box.Text = DBConnect.TableName;

            this.HoursBox.Text = MainWindow.dt.Interval.Hours.ToString();
            this.MinutesBox.Text = MainWindow.dt.Interval.Minutes.ToString();
            this.SecondsBox.Text = MainWindow.dt.Interval.Seconds.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.dt.Start();
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            int hours, minutes, seconds;

            if(IP_Adress_Box.Text != String.Empty)
            {
                if (DBname_Box.Text != String.Empty)
                {
                    if (Username_Box.Text != String.Empty)
                    {
                        if (Password_Box.Text != String.Empty)
                        {
                            if (TableName_Box.Text != String.Empty)
                            {
                                if (int.TryParse(HoursBox.Text, out hours) && int.TryParse(MinutesBox.Text, out minutes) && int.TryParse(SecondsBox.Text, out seconds) &&
                                hours >= 0 && minutes >= 0 && seconds >= 0)
                                {
                                    DBConnect.CloseConnection();
                                    DBConnect.SaveChanges(IP_Adress_Box.Text, DBname_Box.Text, Username_Box.Text, Password_Box.Text,
                                                          TableName_Box.Text, hours, minutes, seconds);
                                    MainWindow.dt.Start();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid time!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Table name!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid password!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username!");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid DataBase name!");
                }
            }else
            {
                MessageBox.Show("Ïnvalid IP!");
            }
            
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.dt.Start();
            this.Close();
        }
    }
}
