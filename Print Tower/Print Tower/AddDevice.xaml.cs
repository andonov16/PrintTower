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
            IPAddress ia;
            if(DeviceName_Block.Text.Length != 0 &&
                !Device.Devices.Any(dev => dev.DeviceName == DeviceName_Block.Text))
            {
                if (IPAddress.TryParse(IP_Block.Text, out ia))
                {
                    Device newDev = new Device(DeviceName_Block.Text, IP_Block.Text);
                    newDev.AddDeviceProperties(newDeviceProps);
                    Device.SaveData();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid IP Adress");
                }
            }else
            {
                MessageBox.Show("Invalid Name");
            }
        }
        private void AddProperty_Button_Click(object sender, RoutedEventArgs e)
        {
            AddDeviceProperty newW = new AddDeviceProperty(newDeviceProps);
            newW.Show();
        }
    }
}
