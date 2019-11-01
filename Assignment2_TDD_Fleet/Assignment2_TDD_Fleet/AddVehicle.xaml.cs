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
        public Vehicle vehicles;
        public static Vehicle vehicle;
        public static bool newVehicle = true;

        public AddVehicle(CarList car, Vehicle vehicle, bool newVehicle)
        {
            InitializeComponent();
            vehicles = vehicle;
            AddVehicle.newVehicle = newVehicle;
        }

        public AddVehicle()
        {
            InitializeComponent();
            vehicles = new Vehicle();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vehicles.vehicleListChanged = true;
            if (newVehicle)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.RegistrationID = int.Parse(TextBoxRegisId.Text);
                vehicle.CarManufacture = TextBoxManufacture.Text;
                vehicle.CarModel = TextBoxModel.Text;
                vehicle.CarYear = int.Parse(TextBoxYear.Text);
                vehicle.FuelType = TextBoxFuelType.Text;
                vehicle.TankCapacity = int.Parse(TextBoxFuelCapacity.Text);
                vehicle.VehicleOdometer = int.Parse(TextBoxOdometer.Text);

                CarList.vehicles.Add(vehicle);
            }
            else
            {
                vehicle.RegistrationID = int.Parse(TextBoxRegisId.Text);
                vehicle.CarManufacture = TextBoxManufacture.Text;
                vehicle.CarModel = TextBoxModel.Text;
                vehicle.CarYear = int.Parse(TextBoxYear.Text);
                vehicle.FuelType = TextBoxFuelType.Text;
                vehicle.TankCapacity = int.Parse(TextBoxFuelCapacity.Text);
                vehicle.VehicleOdometer = int.Parse(TextBoxOdometer.Text);

            }
            //vehicles.SaveVehicles(vehicle.vehicles);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
