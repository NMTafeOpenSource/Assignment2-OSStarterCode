using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
        internal ListView vehicleListView;
        public bool vehicleListChanged;
        
        public Vehicle vehicle;
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
            using (StreamReader r = new StreamReader("../../Vehicles.json"))
            {
                string json = r.ReadToEnd();
                vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(json);
            }
            VehicleListView.ItemsSource = vehicles;
            VehicleListView.Items.Refresh();
        }

        private void AddVehicle_Clicked(object sender, RoutedEventArgs e)
        {
            AddVehicle addVehicle = new AddVehicle();
            addVehicle.ShowDialog();
            VehicleListView.ItemsSource = vehicles;
            VehicleListView.Items.Refresh();
        }

        private void LoadFile_Clicked(object sender, RoutedEventArgs e)
        {
            /*openFileDialog.Filter = "JSON Files (*.json) | *.json";

            if (openFileDialog.ShowDialog() == true)
            {
                vehicles = (List<Vehicle>)JsonConvert.DeserializeObject(File.ReadAllText(openFileDialog.FileName), typeof(List<Vehicle>));
                FileName.Text = openFileDialog.FileName;
                // companyListChanged = false;
            }*/
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
            Vehicle detailsForACompany = button.DataContext as Vehicle;
            vehicles.Remove(detailsForACompany);
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
    }
}
