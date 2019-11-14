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
    /// Interaction logic for VehicleHistory.xaml
    /// </summary>
    public partial class VehicleHistory : Window
    {
        public CarList carlist;
        public BookingList bookingList;
        public List<Journey> journeys;
        public List<Booking> bookings;

        public VehicleHistory()
        {
            InitializeComponent();
        }

        public VehicleHistory(string registrationID, string carManufacture, string carModel, int carYear, string fuelType, double tankCapacity, int vehicleOdometer)
        {
            InitializeComponent();
            TextBoxRegistrationIDHistory.Text = registrationID;
            TextBoxManufactureHistory.Text = carManufacture;
            TextBoxCarModelHistory.Text = carModel;
            TextBoxCarYearHistory.Text = carYear.ToString();
            TextBoxFuelTypeHistory.Text = fuelType;
            TextBoxtankCapacityHistory.Text = tankCapacity.ToString();
            TextBoxVehicleOdometerHistory.Text = vehicleOdometer.ToString();
        }
    }
}
