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
    /// Interaction logic for Bookings.xaml
    /// </summary>
    public partial class Bookings : Window
    {
        public Vehicle vehicles;
        public CarList carList;
        private Vehicle vehicleItem;

        public Bookings(int CarOdometer, string CarModel, string CarManufacture)
        {
            InitializeComponent();
            BookingStartOdometerTextBox.Text = CarOdometer.ToString();
            SelectedVehicleTextBox.Text = CarManufacture + " " + CarModel.ToString();
        }

        public Bookings(CarList carList, Vehicle vehicleItem)
        {
            this.carList = carList;
            this.vehicleItem = vehicleItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
