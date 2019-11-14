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
        public CarList carList;
        public List<Vehicle> vehicles;
        public ViewJourneys viewJourneys;
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
            AddJourney addJourney = new AddJourney(bookingItem.StartRentDate, bookingItem.EndRentDate, bookingItem.StartOdometer, bookingItem.id);
            addJourney.ShowDialog();
        }

        private void ButtonViewJourneys_Clicked(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)sender;
            Booking b = selectedButton.CommandParameter as Booking;
            viewJourneys = new ViewJourneys();
            viewJourneys.Owner = carList;
            viewJourneys.journeys = CarList.journeys.Where(journey => journey.BookingID == b.id).ToList();
            viewJourneys.JourneysListView.ItemsSource = viewJourneys.journeys;
            viewJourneys.ShowDialog();
        }

        private void BookingsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine($"Selected: {e.AddedItems[0]}");
        }

        private void EndBookingButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CarList carList = button.DataContext as CarList;
            Booking booking = button.DataContext as Booking;
            Bookings bookingsEdit = new Bookings(this, booking.id, booking, false);
            bookingsEdit.carList = carList;
            bookingsEdit.ShowDialog();
            bookingListView.ItemsSource = CarList.bookings;
            bookingListView.Items.Refresh();
        }
    }
}
