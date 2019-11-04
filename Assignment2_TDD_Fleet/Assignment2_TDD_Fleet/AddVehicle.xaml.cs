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
    /// Interaction logic for AddVehicle.xaml
    /// </summary>
    public partial class AddVehicle : Window
    {
        public CarList carList;
        public static Vehicle vehicles;
        public static Vehicle vehicle;
        public static bool newVehicle = true;

        public AddVehicle()
        {
            InitializeComponent();
            vehicles = new Vehicle();
        }

        public AddVehicle(CarList car, Vehicle vehicle, bool newVehicle)
        {
            InitializeComponent();
            vehicles = vehicle;
            AddVehicle.newVehicle = newVehicle;
            carList = car;

            TextBoxRegisId.Text = vehicles.RegistrationID;
            TextBoxManufacture.Text = vehicles.CarManufacture;
            TextBoxModel.Text = vehicles.CarModel;
            TextBoxYear.Text = vehicles.CarYear;
            TextBoxFuelType.Text = vehicles.FuelType;
            TextBoxFuelCapacity.Text = vehicles.TankCapacity;
            TextBoxOdometer.Text = vehicles.VehicleOdometer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vehicles.vehicleListChanged = true;
            if (newVehicle)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.RegistrationID = TextBoxRegisId.Text;
                vehicle.CarManufacture = TextBoxManufacture.Text;
                vehicle.CarModel = TextBoxModel.Text;
                vehicle.CarYear = TextBoxYear.Text;
                vehicle.FuelType = TextBoxFuelType.Text;
                vehicle.TankCapacity = TextBoxFuelCapacity.Text;
                vehicle.VehicleOdometer = TextBoxOdometer.Text;

                CarList.vehicles.Add(vehicle);
            }
            else
            {
                vehicles.RegistrationID = TextBoxRegisId.Text;
                vehicles.CarManufacture = TextBoxManufacture.Text;
                vehicles.CarModel = TextBoxModel.Text;
                vehicles.CarYear = TextBoxYear.Text;
                vehicles.FuelType = TextBoxFuelType.Text;
                vehicles.TankCapacity = TextBoxFuelCapacity.Text;
                vehicles.VehicleOdometer = TextBoxOdometer.Text;
            }
            vehicles.SaveVehicles(CarList.vehicles);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
