using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Net;
using System.Net.NetworkInformation;

namespace Print_Tower
{
    /// <summary>
    /// Interaction logic for AddDevice.xaml
    /// </summary>
    public partial class AddDevice : Window
    {
        private ObservableCollection<DeviceProp> newDeviceProps;

        public AddDevice()
        {
            InitializeComponent();
            newDeviceProps = new ObservableCollection<DeviceProp>();
            newDeviceProps.Add(new DeviceProp("Device model", "1.3.6.1.2.1.1.1.0"));
            newDeviceProps.Add(new DeviceProp("Online for", "1.3.6.1.2.1.1.3.0"));
            newDeviceProps.Add(new DeviceProp("Page count", "1.3.6.1.2.1.43.10.2.1.4.1.1"));
            DevicePropsGrid.ItemsSource = newDeviceProps;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.dt.Stop();
                IPAddress ia;
                if (DeviceName_Block.Text.Length != 0)
                {
                    if (Device.Devices != null && Device.Devices.Count > 0)
                    {
                        if (!Device.Devices.Any(dev => dev.DeviceName == DeviceName_Block.Text))
                        {
                            if (IPAddress.TryParse(IP_Block.Text, out ia))
                            {

                                Device newDev = null;
                                try
                                {
                                    newDev = new Device(DeviceName_Block.Text, IP_Block.Text);
                                    newDev.AddDeviceProperties(newDeviceProps);
                                    newDev.CheckOIDs();
                                    Device.SaveData();
                                }
                                catch (NetworkInformationException)
                                {
                                    Device.Devices.Remove(newDev);
                                    MessageBox.Show("Invalid IP Adress");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Invalid IP Adress");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Name");
                        }
                    }
                    else
                    {
                        if (IPAddress.TryParse(IP_Block.Text, out ia))
                        {
                            Device newDev = new Device(DeviceName_Block.Text, IP_Block.Text);
                            newDev.AddDeviceProperties(newDeviceProps);
                            Device.SaveData();
                        }
                        else
                        {
                            MessageBox.Show("Invalid IP Adress");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Name");
                }
                MainWindow.dt.Start();
            }
            catch (Exception)
            {
                MainWindow.dt.Start();
            }


        }
        private void AddProperty_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddDeviceProperty newW = new AddDeviceProperty(newDeviceProps);
                newW.Show();
            }
            catch (Exception)
            {

            }
        }

        private void DevicePropsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Ignore
        }
    }
}
