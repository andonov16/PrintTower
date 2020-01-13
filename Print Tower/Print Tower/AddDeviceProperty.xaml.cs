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

namespace Print_Tower
{
    /// <summary>
    /// Interaction logic for AddDeviceProperty.xaml
    /// </summary>
    public partial class AddDeviceProperty : Window
    {
        ObservableCollection<DeviceProp> devsToAdd;

        public AddDeviceProperty(ObservableCollection<DeviceProp> list)
        {
            InitializeComponent();
            devsToAdd = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            devsToAdd.Add(new DeviceProp(PropName.Text, PropIOD.Text));
            this.Close();
        }
    }
}
