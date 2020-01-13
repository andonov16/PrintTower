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
        DispatcherTimer dt;
        public SettingsWindow(DispatcherTimer dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            dt.Start();
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ia;
            if(IPAddress.TryParse(IP_Adress_Box.Text, out ia))
            {
                if(Username_Box.Text != null && Username_Box.Text != "")
                {
                    if(Password_Box.Text != null && Password_Box.Text != "")
                    {
                        if(TableName_Box.Text != null && TableName_Box.Text != "")
                        {
                            DBConnect.CloseConnection();
                            //DBConnect.ChangeInfo(IP_Adress_Box.Text, Username_Box.Text, Password_Box.Text, TableName_Box.Text);
                            dt.Start();
                            this.Close();
                        }else
                        {
                            MessageBox.Show("Invalid TableName");
                        }
                    }else
                    {
                        MessageBox.Show("Invalid Password");
                    }
                }else
                {
                    MessageBox.Show("Invalid Username");
                }
            }
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
            this.Close();
        }
    }
}
