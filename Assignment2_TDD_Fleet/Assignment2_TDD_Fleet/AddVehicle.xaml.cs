using System.Windows;

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
        public int id;

        public AddVehicle(int newId)
        {
            id = newId;
            InitializeComponent();
            vehicles = new Vehicle();
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vehicles.vehicleListChanged = true;
            if (newVehicle)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Id = id;
                vehicle.RegistrationID = int.Parse(TextBoxRegisId.Text);
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
                vehicles.RegistrationID = int.Parse(TextBoxRegisId.Text);
                vehicles.CarManufacture = TextBoxManufacture.Text;
                vehicles.CarModel = TextBoxModel.Text;
                vehicles.CarYear = int.Parse(TextBoxYear.Text);
                vehicles.FuelType = TextBoxFuelType.Text;
                vehicles.TankCapacity = double.Parse(TextBoxFuelCapacity.Text);
                vehicles.VehicleOdometer = int.Parse(TextBoxOdometer.Text);
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
