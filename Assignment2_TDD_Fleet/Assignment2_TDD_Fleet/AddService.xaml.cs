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
    /// Interaction logic for AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {
        public Guid id;
        public static Service services;
        public int vehicleOdometer;
        public VehicleHistory vehicleHistory;
        public CarList carList;

        /// <summary>
        /// this is a constructor for addService window
        /// </summary>
        public AddService()
        {
            InitializeComponent();

        }
        /// <summary>
        /// this is a constructor for this window to get items from carList
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicleOdometer"></param>
        public AddService(Guid id, int vehicleOdometer)
        {
            InitializeComponent();
            // set the textbox for service as same as the vehicle odometer on carList
            TextBoxLastOdometerForService.Text = vehicleOdometer.ToString();
            this.id = id;
            this.vehicleOdometer = vehicleOdometer;
        }
        /// <summary>
        /// this is a click event for close add service window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceCancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// this is click event to add service/service now!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceNowButton_Clicked(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.vehicleID = id;
            service.ServiceOdometer = int.Parse(TextBoxLastOdometerForService.Text);
            service.ServiceDate = DateTime.Parse(DatePickerForLastServiceDate.Text);

            CarList.services.Add(service);
            // save new service to list of services
            Service.SaveServices(CarList.services);
            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == service.vehicleID);
            List<Service> relatedServicesWithVehicle = CarList.services.FindAll(s => s.vehicleID == relatedVehicle.Id);
            relatedVehicle.updateServicesCount(relatedServicesWithVehicle);
            vehicleHistory = new VehicleHistory();
            vehicleHistory.servicesListView.ItemsSource = CarList.services;
            vehicleHistory.servicesListView.Items.Refresh();
            Close();
        }
    }
}
