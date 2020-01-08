using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.ComponentModel;

namespace Print_Tower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw;
        public MainWindow()
        {
            InitializeComponent();
            Device.LoadData();
            PrintersList.ItemsSource = Device.Devices;
            BWorker.StartWorking(RefreshInfo);
        }

        private void RefreshInfo()
        {
            foreach (var dev in Device.Devices)
                dev.CheckOIDs();
            Device.SaveData();
            Thread.Sleep(1000);
            RefreshInfo();
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
        private void Explore_Button_Click(object sender, RoutedEventArgs e)
        {
            if((Device) PrintersList.SelectedItem != null)
            {
                ExploreWindow ew = new ExploreWindow((Device)PrintersList.SelectedItem);
                ew.Show();
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
