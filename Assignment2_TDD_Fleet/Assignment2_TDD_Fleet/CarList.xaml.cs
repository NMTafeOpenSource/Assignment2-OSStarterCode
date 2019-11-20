using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Assignment2_TDD_Fleet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CarList : Window
    {
        public static List<Vehicle> vehicles;
        public static List<Booking> bookings;
        public static List<Journey> journeys;
        public static List<FuelPurchase> fuelPurchases;
        public static List<Service> services;
        public ListView vehicleListView;
        public Booking booking;
        public ViewJourneys viewJourneys;
        public VehicleHistory vehicleHistory;
        public BookingList bookingList;
        public bool vehicleListChanged;
        public Vehicle vehicle;
        string vehiclesFileName = "Vehicles.json";
        string bookingFileName = "Bookings.json";
        string journeysFileName = "Journey.json";
        string fuelPurchasesFileName = "FuelPurchases.json";
        string servicesFileName = "Service.json";
        internal SaveFileDialog saveFileDialog = new SaveFileDialog();
        internal OpenFileDialog openFileDialog = new OpenFileDialog();


        public CarList()
        {
            InitializeComponent();
            ScanStatusKeysInBackground();
            vehicleListView = VehicleListView;
            VehicleListView.ItemsSource = vehicles;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(VehicleListView.ItemsSource);
            vehicles = new List<Vehicle>();
            bookings = new List<Booking>();
            journeys = new List<Journey>();
            fuelPurchases = new List<FuelPurchase>();
            services = new List<Service>();
            LoadJourneys();
            LoadBooking();
            LoadVehicle();
            LoadFuelPurchases();
            LoadServices();
            updateOdometer();
            updateRentalCosts();
        }


        public void CheckKeyStatus()
        {
            var isNumLockToggled = Keyboard.IsKeyToggled(Key.NumLock); // variable for NumLock
            var isScrollLockToggled = Keyboard.IsKeyToggled(Key.Scroll); // variable for ScrollLock
            var isCapsLockToggled = Keyboard.IsKeyToggled(Key.CapsLock); // variable for CapsLock

            if (isNumLockToggled)
            {
                // if the NumLock is toggled on keyboard
                // The text color will change into red
                NumLockStatus.Foreground = Brushes.Red;
            }
            else
            {
                // if you untoggle the NumLock on your keyboard
                // The text color will change into gray
                NumLockStatus.Foreground = Brushes.Gray;
            }

            if (isCapsLockToggled)
            {
                // if the CapsLock is toggled on keyboard
                // The text color will change into red
                CapsLockStatus.Foreground = Brushes.Red;
            }
            else
            {
                // if you untoggle the CapsLock on your keyboard
                // The text color will change into gray
                CapsLockStatus.Foreground = Brushes.Gray;
            }

            if (isScrollLockToggled)
            {
                // if the ScrollLock is toggled on keyboard
                // The text color will change into red
                ScrollLockStatus.Foreground = Brushes.Red;
            }
            else
            {
                // if you untoggle the ScrollLock on your keyboard
                // The text color will change into gray
                ScrollLockStatus.Foreground = Brushes.Gray;
            }
        }

        private async Task ScanStatusKeysInBackground()
        {
            while (true)
            {
                CheckKeyStatus();
                await Task.Delay(100);
            }
        }

        public void LoadVehicle()
        {
            vehicles.Clear();
            // deserialize JSON directly from a file
            vehicles = (List<Vehicle>)JsonConvert.DeserializeObject(File.ReadAllText(vehiclesFileName), typeof(List<Vehicle>));
            vehicleListView.ItemsSource = vehicles;
            vehicleListView.Items.Refresh();
        }

        private void AddVehicle_Clicked(object sender, RoutedEventArgs e)
        {
            Guid id = Guid.NewGuid();
            AddVehicle addVehicle = new AddVehicle(id);
            addVehicle.ShowDialog();
            VehicleListView.ItemsSource = vehicles;
            VehicleListView.Items.Refresh();
        }

        private void LoadFile_Clicked(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "JSON Files (*.json) | *.json";

            if (openFileDialog.ShowDialog() == true)
            {
                vehicles = (List<Vehicle>)JsonConvert.DeserializeObject(File.ReadAllText(openFileDialog.FileName), typeof(List<Vehicle>));
            }
            VehicleListView.ItemsSource = vehicles;
            VehicleListView.Items.Refresh();
        }

        private void SaveFile_Clicked(object sender, RoutedEventArgs e)
        {
            saveFileDialog.Filter = "JSON Files (*.json) | *.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter file = File.CreateText(saveFileDialog.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, vehicles);
                }
                vehicleListChanged = false;
            }
        }

        private void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Vehicle detailsForAVehicle = button.DataContext as Vehicle;
            vehicles.Remove(detailsForAVehicle);
            CollectionViewSource.GetDefaultView(vehicleListView.ItemsSource).Refresh();
            vehicleListChanged = true;
            detailsForAVehicle.SaveVehicles(vehicles);
        }

        private void ButtonEdit_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Vehicle vehicleItem = button.DataContext as Vehicle;
            AddVehicle vehicleAdd = new AddVehicle(this, vehicleItem, false);
            vehicleAdd.carList = this;
            vehicleAdd.ShowDialog();
            vehicleListView.ItemsSource = vehicles;
            vehicleListView.Items.Refresh();
        }

        private void Filter_Text_Changed(object sender, TextChangedEventArgs e)
        {
            string textSearch = FilterTextBox.Text;
            List<Vehicle> matches = CarList.vehicles.FindAll(vehicles
                => Regex.Matches(vehicles.CarManufacture.ToLower(), textSearch.ToLower()).Count > 0).ToList();
            VehicleListView.ItemsSource = matches;
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = "";
            VehicleListView.ItemsSource = vehicles;
        }

        private void BtnRentVehicle_Click(object sender, RoutedEventArgs e)
        {
            Guid NewId = Guid.NewGuid();
            Button button = sender as Button;
            Vehicle vehicleItem = button.DataContext as Vehicle;
            Bookings bookings = new Bookings(vehicleItem.VehicleOdometer, vehicleItem.CarModel, vehicleItem.CarManufacture, vehicleItem.Id, NewId);
            bookings.vehicleId = vehicleItem.Id;
            bookings.ShowDialog();
        }

        public void LoadBooking()
        {
            bookings = (List<Booking>)JsonConvert.DeserializeObject(File.ReadAllText(bookingFileName), typeof(List<Booking>));
        }

        private void BookingList_Clicked(object sender, RoutedEventArgs e)
        {
            bookingList = new BookingList();
            bookingList.Owner = this;
            bookingList.bookings = CarList.bookings;
            bookingList.BookingsListView.ItemsSource = CarList.bookings;
            bookingList.vehicleListView = VehicleListView;
            bookingList.ShowDialog();
        }

        public void LoadJourneys()
        {
            journeys = (List<Journey>)JsonConvert.DeserializeObject(File.ReadAllText(journeysFileName), typeof(List<Journey>));
        }

        public void LoadFuelPurchases()
        {
            fuelPurchases = (List<FuelPurchase>)JsonConvert.DeserializeObject(File.ReadAllText(fuelPurchasesFileName), typeof(List<FuelPurchase>));
        }

        public void LoadServices()
        {
            services = (List<Service>)JsonConvert.DeserializeObject(File.ReadAllText(servicesFileName), typeof(List<Service>));
        }
        private void ViewButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)sender;
            Vehicle v = selectedButton.CommandParameter as Vehicle;
            vehicleHistory = new VehicleHistory(v.RegistrationID, v.CarManufacture, v.CarModel, v.CarYear, v.FuelType, v.TankCapacity, v.VehicleOdometer, v.serviceCount);
            vehicleHistory.Owner = this;
            vehicleHistory.journeys = journeys.Where(journey => journey.vehicleID == v.Id).ToList();
            vehicleHistory.JourneysListViewForHistory.ItemsSource = vehicleHistory.journeys;
            vehicleHistory.bookings = bookings.Where(booking => booking.Vehicleid == v.Id).ToList();
            vehicleHistory.BookingsListViewForHistory.ItemsSource = vehicleHistory.bookings;
            vehicleHistory.fuelPurchases = fuelPurchases.Where(fuelP => fuelP.VId == v.Id).ToList();
            vehicleHistory.FuelPurchasesViewForHistory.ItemsSource = vehicleHistory.fuelPurchases;
            vehicleHistory.services = services.Where(service => service.vehicleID == v.Id).ToList();
            vehicleHistory.servicesListView .ItemsSource= vehicleHistory.services;
            vehicleHistory.ShowDialog();
        }

        public void updateOdometer()
        {
            if (CarList.vehicles.Count > 0)
            {
                vehicles.ForEach(v =>
                {
                    List<Journey> associatedJourneys = CarList.journeys.FindAll(j => j.vehicleID == v.Id).ToList<Journey>();
                    List<Journey> journeysUpToToday = associatedJourneys
                    .Where(j => DateTime.Compare(j.JourneyEndedAt, DateTime.Now) <= 0)
                    .ToList<Journey>();

                    DateTime latestJourneyDate;

                    if (journeysUpToToday.Count > 0)
                    {
                        latestJourneyDate = journeysUpToToday.Max(j => j.JourneyEndedAt).Date;
                        Journey latestJourney = associatedJourneys.Find(j => (j.JourneyEndedAt - latestJourneyDate).TotalDays == 0);
                        v.VehicleOdometer = latestJourney.EndOdometer;
                    }
                });

            }
            VehicleListView.Items.Refresh();
        }

        public void updateRentalCosts()
        {
            vehicles.ForEach(v =>
            {
                List<Booking> associatedBookings = bookings.FindAll(b => b.Vehicleid == v.Id).ToList<Booking>();
                v.updateTotalRentCost(associatedBookings);
            });
        }

        private void ButtonService_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Vehicle vehicleService = button.DataContext as Vehicle;
            vehicleHistory = new VehicleHistory();
            AddService addService = new AddService(vehicleService.Id, vehicleService.VehicleOdometer);
            addService.ShowDialog();
        }

        private void buttonPrintVehicle_Click(object sender, RoutedEventArgs e)
        {
            Button printButton = (Button)sender;
            Vehicle v = printButton.CommandParameter as Vehicle;
            PrintDetails printDetails = new PrintDetails();
            printDetails.vehicle = v;
            printDetails.ShowDialog();
        }
    }
}
