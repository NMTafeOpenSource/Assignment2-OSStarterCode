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
        public Guid bookingId;
        public Guid vehicleId;
        public int vehicleOdometer;

        public Bookings()
        {
            InitializeComponent();
            
        }

        public Bookings(int CarOdometer, string CarModel, string CarManufacture)
        {
            InitializeComponent();
            BookingStartOdometerTextBox.Text = CarOdometer.ToString();
            SelectedVehicleTextBox.Text = CarManufacture + " " + CarModel.ToString();
            ComboBoxRentalType.Items.Add(Booking.BookingType.Day);
            ComboBoxRentalType.Items.Add(Booking.BookingType.Km);
            bookings = new Booking();
            vehicles = new Vehicle();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        public Bookings(int CarOdometer, string CarModel, string CarManufacture, Guid id, Guid NewId) : this(CarOdometer, CarModel, CarManufacture)
        {
            vehicleId = id;
            bookingId = NewId;
            InitializeComponent();
            bookings = new Booking();
            vehicles = new Vehicle();
        }

        //public Bookings(BookingList bookingList, Guid id, Booking booking, bool newBooking)
        //{
        //    InitializeComponent();
        //    bookings = new Booking();
        //    this.bookingList = bookingList;
        //    Bookings.newBooking = newBooking;
        //    vehicleID = id;
        //    bookings = booking;


        //    CustomerNameTextBox.Text = bookings.CustomerName;
        //    BookingStartOdometerTextBox.Text = bookings.StartOdometer.ToString();
        //    SelectedVehicleTextBox.Text = bookings.SelectedVehicle;
        //    BookingStartDatePicker.Text = bookings.StartRentDate.ToString();
        //    BookingEndDatePicker.Text = bookings.EndRentDate.ToString();
        //    ComboBoxRentalType.Text = bookings.RentalType;
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           bookings.bookingListChanged = true;
           if (newBooking)
           {
                Booking booking = new Booking();
                booking.id = bookingId;
                booking.Vehicleid = vehicleId;
                booking.CustomerName = CustomerNameTextBox.Text;
                booking.StartOdometer = int.Parse(BookingStartOdometerTextBox.Text);
                booking.SelectedVehicle = SelectedVehicleTextBox.Text;
                booking.StartRentDate = DateTime.Parse(BookingStartDatePicker.Text);
                booking.EndRentDate = DateTime.Parse(BookingEndDatePicker.Text);
                booking.RentalType = ComboBoxRentalType.Text;

                CarList.bookings.Add(booking);
           }
           else
           {
                bookings.CustomerName = CustomerNameTextBox.Text;
                bookings.StartOdometer = int.Parse(BookingStartOdometerTextBox.Text);
                bookings.SelectedVehicle = SelectedVehicleTextBox.Text;
                bookings.StartRentDate = DateTime.Parse(BookingStartDatePicker.Text);
                bookings.EndRentDate = DateTime.Parse(BookingEndDatePicker.Text);
                //bookings.RentalType = booking.RentalType.ToString();
                bookings.EndOdometer = int.Parse(BookingEndOdometerTextBox.Text);

           }
           bookings.SaveBookings(CarList.bookings);
           Close();
        }
    }
}
