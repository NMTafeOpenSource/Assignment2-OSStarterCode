using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        internal ListView vehicleListView;
        public Booking booking;
        public ViewJourneys viewJourneys;
        public BookingList bookingList;
        public bool vehicleListChanged;
        public Vehicle vehicle;
        string vehiclesFileName = "jsontestshit.json";
        string bookingFileName = "Bookings.json";
        string journeysFileName = "Journey.json";
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
            LoadJourneys();
            LoadBooking();
            LoadVehicle();
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
            //int newId = vehicles.Max(x => x.Id) + 1;
            //Journey journeyItems = DataContext as Journey;
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
                //FileNameLabel.Text = openFileDialog.FileName;
                // companyListChanged = false;
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
            //this.SaveCompanies(MainWindow.companies);
            CollectionViewSource.GetDefaultView(vehicleListView.ItemsSource).Refresh();
            vehicleListChanged = true;
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
            //Guid Id = Guid.NewGuid();
            Button button = sender as Button;
            Vehicle vehicleItem = button.DataContext as Vehicle;
            Bookings bookings = new Bookings(vehicleItem.VehicleOdometer, vehicleItem.CarModel, vehicleItem.CarManufacture, vehicleItem.Id);
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
            //CarList.bookings = CarList.bookings.Concat(bookingList.bookingsFromJSONFile).ToList();
            bookingList.bookings = CarList.bookings;
            bookingList.BookingsListView.ItemsSource = CarList.bookings;
            bookingList.ShowDialog();
        }

        public void LoadJourneys()
        {
            journeys = (List<Journey>)JsonConvert.DeserializeObject(File.ReadAllText(journeysFileName), typeof(List<Journey>));
        }

        private void ViewButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)sender;
            Vehicle v = selectedButton.CommandParameter as Vehicle;
            viewJourneys = new ViewJourneys();
            viewJourneys.Owner = this;
            viewJourneys.journeys = journeys.Where(x => x.BookingID == v.Id).ToList();
            viewJourneys.JourneysListView.ItemsSource = viewJourneys.journeys;
            viewJourneys.ShowDialog();
        }
    }
}
