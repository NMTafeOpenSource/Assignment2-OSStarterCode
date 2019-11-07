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
        public BookingList bookingList;
        public static Booking bookings; 
        public static Vehicle vehicles;
        public static bool newBooking = true;
        public CarList carList;
        private Vehicle vehicleItem;

        public Bookings()
        {
            InitializeComponent();
        }

        public Bookings(int CarOdometer, string CarModel, string CarManufacture)
        {
            InitializeComponent();
            BookingStartOdometerTextBox.Text = CarOdometer.ToString();
            SelectedVehicleTextBox.Text = CarManufacture + " " + CarModel.ToString();
            bookings = new Booking();

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           bookings.bookingListChanged = true;
           if (newBooking)
           {
                Booking booking = new Booking();
                booking.CustomerName = CustomerNameTextBox.Text;
                booking.StartOdometer = int.Parse(BookingStartOdometerTextBox.Text);
                booking.SelectedVehicle = SelectedVehicleTextBox.Text;
                booking.StartRentDate = DateTime.Parse(BookingStartDatePicker.Text);
                booking.EndRentDate = DateTime.Parse(BookingEndDatePicker.Text);

                CarList.bookings.Add(booking);
                //bookingList.bookingListView.Items.Refresh();
           }
           else
           {
                bookings.CustomerName = CustomerNameTextBox.Text;
                bookings.StartOdometer = int.Parse(BookingStartOdometerTextBox.Text);
                bookings.SelectedVehicle = SelectedVehicleTextBox.Text;
                bookings.StartRentDate = DateTime.Parse(BookingStartDatePicker.Text);
                bookings.EndRentDate = DateTime.Parse(BookingEndDatePicker.Text);
           }
           bookings.SaveBookings(CarList.bookings);
           Close();
        }
    }
}
