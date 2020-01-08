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

namespace Print_Tower
{
    /// <summary>
    /// Interaction logic for ExploreWindow.xaml
    /// </summary>
    public partial class ExploreWindow : Window
    {
        private Device exploredDev;

        public ExploreWindow(Device dev)
        {
            InitializeComponent();
            exploredDev = dev;
            DeviceName_Block.Text = dev.DeviceName;
            DeviceIP_Block.Text = dev.IP;
            DevPropsGrid.ItemsSource = exploredDev.DeviceProps;
        }
    }
}
