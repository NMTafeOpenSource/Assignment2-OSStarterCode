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
        public ListView bookingListView;
        public bool bookingListChanged;
        public Guid selectedId;

        /// <summary>
        /// this is a constructor for this window
        /// </summary>
        public BookingList()
        {
            InitializeComponent();
            bookingListView = BookingsListView;
        }
        /// <summary>
        /// this is an event for filter textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterBoxBookingList_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = FilterTextBoxBookingList.Text;
            List<Booking> matches = CarList.bookings.FindAll(bookings 
                => Regex.Matches(bookings.SelectedVehicle.ToLower(), textSearch.ToLower()).Count > 0).ToList();
            bookingListView.ItemsSource = matches;
        }
        /// <summary>
        /// this is a click event to open addJourney Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// this is a click event for viewJourneys window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// this is an event that i used to get the first selected item
        /// just for debugging, ignore it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookingsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine($"Selected: {e.AddedItems[0]}");
        }
        /// <summary>
        /// this is a click event to open fuel purchases window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuelPurchases_Clicked(object sender, RoutedEventArgs e)
        {
            Button fuelButton = (Button)sender;
            Booking f = fuelButton.CommandParameter as Booking;
            FuelPurchases fuelPurchases = new FuelPurchases(f.Vehicleid);
            fuelPurchases.Owner = (CarList)this.Owner;
            fuelPurchases.ShowDialog();
        }
        /// <summary>
        /// this is a click event to delete selected row of booking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteBooking_Clicked(object sender, RoutedEventArgs e)
        {
            Button deleteBookingButton = sender as Button;
            Booking detailsForBooking = deleteBookingButton.DataContext as Booking;
            deleteBooking(detailsForBooking);
            CollectionViewSource.GetDefaultView(BookingsListView.ItemsSource).Refresh();
            bookingListChanged = true;
            detailsForBooking.SaveBookings(bookings);
            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == detailsForBooking.Vehicleid);
            List<Booking> allBookingsWithRelatedVehicle = CarList.bookings != null && CarList.bookings.Count > 0 ? CarList.bookings.FindAll(b => b.Vehicleid == relatedVehicle.Id): null;
            Journey allJourneysRelatedWithBooking = CarList.journeys != null && CarList.journeys.Count > 0 ?CarList.journeys.Find(j => j.BookingID == detailsForBooking.id): null;
            if (allJourneysRelatedWithBooking != null)
            {
                CarList.journeys.Remove(allJourneysRelatedWithBooking);
                allJourneysRelatedWithBooking.SaveJourney(CarList.journeys);
            }
            relatedVehicle.updateTotalRentCost(allBookingsWithRelatedVehicle);
            relatedVehicle.SaveVehicles(CarList.vehicles);
            vehicleListView.ItemsSource = CarList.vehicles;
            vehicleListView.Items.Refresh();
        }
        /// <summary>
        /// test bool to check if the delete function is working or not
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public static bool deleteBooking(Booking booking)
        {
            return CarList.bookings.Remove(booking);
        }
    }
}
