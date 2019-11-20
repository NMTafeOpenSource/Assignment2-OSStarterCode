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

namespace Assignment2_TDD_Fleet
{
    /// <summary>
    /// Interaction logic for PrintDetails.xaml
    /// </summary>
    public partial class PrintDetails : Window
    {
        public Vehicle vehicle;
        public PrintDetails()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (vehicle != null)
            {
                textBlockVehicleDetails.Text = vehicle.printDetails();
            }
        }
    }
}
