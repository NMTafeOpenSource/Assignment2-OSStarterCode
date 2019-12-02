using System;
using System.Windows;

namespace Assignment2_TDD_Fleet
{
    /// <summary>
    /// Interaction logic for AddVehicle.xaml
    /// </summary>
    public partial class AddVehicle : Window
    {
        public CarList carList;
        public Booking booking;
        public static Vehicle vehicles;
        public static Vehicle vehicle;
        public static bool newVehicle = true;
        public Guid id;
        
        /// <summary>
        /// this is a constructor for this window and getting
        /// items from carList, vehicle, and boolean for new vehicle
        /// </summary>
        /// <param name="car"></param>
        /// <param name="vehicle"></param>
        /// <param name="newVehicle"></param>
        public AddVehicle(CarList car, Vehicle vehicle, bool newVehicle)
        {
            InitializeComponent();
            vehicles = vehicle;
            AddVehicle.newVehicle = newVehicle;
            carList = car;

            TextBoxRegisId.Text = vehicles.RegistrationID.ToString();
            TextBoxManufacture.Text = vehicles.CarManufacture;
            TextBoxModel.Text = vehicles.CarModel;
            TextBoxYear.Text = vehicles.CarYear.ToString();
            TextBoxFuelType.Text = vehicles.FuelType;
            TextBoxFuelCapacity.Text = vehicles.TankCapacity.ToString();
            TextBoxOdometer.Text = vehicles.VehicleOdometer.ToString();
        }
        /// <summary>
        /// this is constructor for this window
        /// to get guid of vehicle
        /// </summary>
        /// <param name="id"></param>
        public AddVehicle(Guid id)
        {
            this.id = id;
            //this.odometer = EndOdometer;
            InitializeComponent();
            vehicles = new Vehicle();
        }
        /// <summary>
        /// this is a click event to add new vehicle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vehicles.vehicleListChanged = true;
            if (newVehicle)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Id = id;
                vehicle.RegistrationID = TextBoxRegisId.Text;
                vehicle.CarManufacture = TextBoxManufacture.Text;
                vehicle.CarModel = TextBoxModel.Text;
                vehicle.CarYear = int.Parse(TextBoxYear.Text);
                vehicle.FuelType = TextBoxFuelType.Text;
                vehicle.TankCapacity = double.Parse(TextBoxFuelCapacity.Text);
                vehicle.VehicleOdometer = int.Parse(TextBoxOdometer.Text);

                CarList.vehicles.Add(vehicle);
            }
            else
            {
                vehicles.RegistrationID = TextBoxRegisId.Text;
                vehicles.CarManufacture = TextBoxManufacture.Text;
                vehicles.CarModel = TextBoxModel.Text;
                vehicles.CarYear = int.Parse(TextBoxYear.Text);
                vehicles.FuelType = TextBoxFuelType.Text;
                vehicles.TankCapacity = double.Parse(TextBoxFuelCapacity.Text);
                vehicles.VehicleOdometer = int.Parse(TextBoxOdometer.Text);
            }
            // save the new vehicle to list of vehicles on carList window
            vehicles.SaveVehicles(CarList.vehicles);
            Close();
        }
        /// <summary>
        /// this is a click event to close the add vehicle form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // close the window
            Close();
        }
    }
}
