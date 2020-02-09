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
        public static DispatcherTimer dt;

        public MainWindow()
        {
            dt = new DispatcherTimer();
            InitializeComponent();
            Device.LoadData();            
            PrintersList.ItemsSource = Device.Devices;
            dt.Tick += RefreshInfo;
            dt.Interval = new TimeSpan(1, 0, 0);
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
                        if (dev.DeviceProps.Count >= 3)
                        {
                            DBConnect.Insert(dev.DeviceName, dev.IP, dev.Online,
                            dev.DeviceProps[0].PropName, dev.DeviceProps[0].PropValue,
                            dev.DeviceProps[1].PropName, dev.DeviceProps[1].PropValue,
                            dev.DeviceProps[2].PropName, dev.DeviceProps[2].PropValue,
                            DateTime.Now);
                        }
                        switch (dev.DeviceProps.Count)
                        {
                            case 1:
                                DBConnect.Insert(dev.DeviceName, dev.IP, dev.Online,
                                dev.DeviceProps[0].PropName, dev.DeviceProps[0].PropValue,
                                null, null, null, null,
                                DateTime.Now); break;
                            case 2:
                                DBConnect.Insert(dev.DeviceName, dev.IP, dev.Online,
                                dev.DeviceProps[0].PropName, dev.DeviceProps[0].PropValue,
                                dev.DeviceProps[1].PropName, dev.DeviceProps[1].PropValue,
                                null, null,
                                DateTime.Now); break;
                        }
                    }
                    Device.SaveData();
                }
            }
            catch (Exception)
            {
                //Ignore
            }
        }


        //----------------------------MainWindow Events---------------------------------------------------
        private void DeleteDevice_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Device.Delete((Device)PrintersList.SelectedItem);
                Device.SaveData();
            }
            catch (Exception)
            {
                //ignore the problem
            }
        }
        private void AddDevice_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddDevice dv = new AddDevice();
                dv.Show();
            }
            catch (Exception)
            {
               // MessageBox.Show("Process failed! Please try again!");
            }
        }
        private void DevPropsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
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
            catch (Exception)
            {
               //MessageBox.Show("Process failed! Please try again!");
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
                //MessageBox.Show("Process failed! Please try again!");
            }
        }
        private void DeleteProp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeviceProp deletedProp = (DeviceProp)DevPropsGrid.SelectedItem;
                if (deletedProp != null)
                {
                    Device dev = (Device)PrintersList.SelectedItem;
                    dev.DeviceProps.Remove(deletedProp);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Process failed! Please try again!");
            }
        }
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dt.Stop();
                SettingsWindow sw = new SettingsWindow();
                sw.Show();
            }
            catch (Exception)
            {
            }
        }
        private void PrintersList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Ignore
        }
    }
}
