using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Print_Tower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Device.LoadData();            
            PrintersList.ItemsSource = Device.Devices;
            dt.Tick += RefreshInfo;
            dt.Interval = new TimeSpan(0, 10, 0);
            dt.Start();
        }

        private void RefreshInfo(object sender, EventArgs e)
        {
            try
            {
                if (Device.Devices != null && Device.Devices.Count > 0)
                {
                    foreach (var dev in Device.Devices)
                    {
                        dev.CheckOIDs();
                        DBConnect.Update(dev.DeviceName, "Status", dev.Online.ToString());
                        if (dev.DeviceProps.Count >= 3)
                            for (int i = 0; i < 3; i++)
                            {
                                DBConnect.Update(dev.DeviceName, "Prop" + i + "Name", dev.DeviceProps[i].PropName);
                                DBConnect.Update(dev.DeviceName, "Prop" + i + "Value", dev.DeviceProps[i].PropValue);
                            }
                        else
                        {
                            for (int i = 0; i < dev.DeviceProps.Count; i++)
                            {
                                DBConnect.Update(dev.DeviceName, "Prop" + i + "Name", dev.DeviceProps[i].PropName);
                                DBConnect.Update(dev.DeviceName, "Prop" + i + "Value", dev.DeviceProps[i].PropValue);
                            }
                        }
                        DBConnect.Update(dev.DeviceName, "LastCheck", DateTime.Now.ToString());
                    }
                    Device.SaveData();
                }
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        private void DeleteDevice_Button_Click(object sender, RoutedEventArgs e)
        {
            Device.Delete((Device)PrintersList.SelectedItem);
            Device.SaveData();
        }
        private void AddDevice_Button_Click(object sender, RoutedEventArgs e)
        {
            AddDevice dv = new AddDevice();
            dv.Show();
        }
        private void DevPropsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Device exploredDev = (Device)PrintersList.SelectedItem;
            if (exploredDev != null)
            {
                if (ExplorePanel.Visibility == Visibility.Hidden)
                    ExplorePanel.Visibility = Visibility.Visible;
                DeviceName_Block.Text = exploredDev.DeviceName;
                DeviceIP_Block.Text = exploredDev.IP;
                DevPropsGrid.ItemsSource = exploredDev.DeviceProps;
            }
            else
            {
                ExplorePanel.Visibility = Visibility.Hidden;
            }
        }

        private void AddProp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Device dev = (Device)PrintersList.SelectedItem;
                AddDeviceProperty addPropWin = new AddDeviceProperty(dev.DeviceProps);
                addPropWin.Show();
            }
            catch (Exception)
            {
                //ingnore the click
            }
        }
        private void DeleteProp_Click(object sender, RoutedEventArgs e)
        {
            DeviceProp deletedProp = (DeviceProp)DevPropsGrid.SelectedItem;
            if(deletedProp != null)
            {
                Device dev = (Device)PrintersList.SelectedItem;
                dev.DeviceProps.Remove(deletedProp);
            }
        }
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
            SettingsWindow sw = new SettingsWindow(dt);
            sw.Show();
        }
    }
}
