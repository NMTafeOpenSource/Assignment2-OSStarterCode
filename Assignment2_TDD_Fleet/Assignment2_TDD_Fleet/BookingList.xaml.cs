using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BookingList.xaml
    /// </summary>
    public partial class BookingList : Window
    {
        public List<Vehicle> vehicles;
        public ViewJourneys viewJourneys;
        public ListView vehicleListView;
        public List<Booking> bookings;
        public List<Journey> journeys;
        public List<Booking> bookingsFromJSONFile;
        //string journeysFileName = "Journey.json";
        public ListView bookingListView;
        public bool bookingListChanged;
        public Guid selectedId;

        public BookingList()
        {
            InitializeComponent();
            bookingListView = BookingsListView;
            
            // BookingsListView.ItemsSource = bookings;
            // CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(BookingsListView.ItemsSource);
            // bookings = new List<Booking>();
        }

        private void FilterBoxBookingList_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = FilterTextBoxBookingList.Text;
            List<Booking> matches = CarList.bookings.FindAll(bookings 
                => Regex.Matches(bookings.SelectedVehicle.ToLower(), textSearch.ToLower()).Count > 0).ToList();
            bookingListView.ItemsSource = matches;
        }

        private void ButtonForJourneys_Click(object sender, RoutedEventArgs e)
        {
            //Guid JourneyId = new Guid(); 
            Button button = sender as Button;
            Booking bookingItem = button.DataContext as Booking;
            AddJourney addJourney = new AddJourney(bookingItem.StartRentDate, bookingItem.EndRentDate, bookingItem.StartOdometer, bookingItem.id, bookingItem.Vehicleid);
            addJourney.Owner = (CarList)this.Owner;
            addJourney.BookingID = bookingItem.id;
            addJourney.ShowDialog();
        }

        private void ButtonViewJourneys_Clicked(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)sender;
            Booking b = selectedButton.CommandParameter as Booking;
            viewJourneys = new ViewJourneys();
            viewJourneys.Owner = (CarList)this.Owner;
            viewJourneys.BookingID = b.id;
            viewJourneys.journeys = CarList.journeys.Where(journey => journey.BookingID == b.id).ToList();
            viewJourneys.JourneysListView.ItemsSource = viewJourneys.journeys;
            viewJourneys.vehicleListView = vehicleListView;
            viewJourneys.ShowDialog();
        }

        private void BookingsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine($"Selected: {e.AddedItems[0]}");
        }

        private void FuelPurchases_Clicked(object sender, RoutedEventArgs e)
        {
            Button fuelButton = (Button)sender;
            Booking f = fuelButton.CommandParameter as Booking;
            FuelPurchases fuelPurchases = new FuelPurchases(f.Vehicleid);
            fuelPurchases.Owner = (CarList)this.Owner;
            fuelPurchases.ShowDialog();
        }

        private void ButtonDeleteBooking_Clicked(object sender, RoutedEventArgs e)
        {
            Button deleteBookingButton = sender as Button;
            Booking detailsForBooking = deleteBookingButton.DataContext as Booking;
            CarList.bookings.Remove(detailsForBooking);
            CollectionViewSource.GetDefaultView(BookingsListView.ItemsSource).Refresh();
            bookingListChanged = true;
            detailsForBooking.SaveBookings(bookings);
            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == detailsForBooking.Vehicleid);
            List<Booking> allBookingsWithRelatedVehicle = CarList.bookings.FindAll(b => b.Vehicleid == relatedVehicle.Id);
            relatedVehicle.updateTotalRentCost(allBookingsWithRelatedVehicle);
            relatedVehicle.SaveVehicles(CarList.vehicles);
            vehicleListView.ItemsSource = CarList.vehicles;
            vehicleListView.Items.Refresh();
        }
    }
}
