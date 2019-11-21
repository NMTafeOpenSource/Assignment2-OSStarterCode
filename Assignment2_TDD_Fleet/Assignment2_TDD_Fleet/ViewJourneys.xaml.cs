using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ViewJourneys.xaml
    /// </summary>
    public partial class ViewJourneys : Window
    {
        public CarList carlist;
        public BookingList bookingList;
        public static Journey Journey;
        public List<Booking> bookings;
        public List<Journey> journeys;
        public ListView journeysListView;
        public ListView vehicleListView;
        public bool journeysListChanged;
        public Guid BookingID;

        /// <summary>
        /// this is a constructor for this window
        /// </summary>
        public ViewJourneys()
        {
            InitializeComponent();
            journeysListView = JourneysListView;
            
        }
        /// <summary>
        /// this is a click event to delete journey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteJourney_Clicked(object sender, RoutedEventArgs e)
        {
            Button deleteJourneyButton = sender as Button;
            Journey j = deleteJourneyButton.CommandParameter as Journey;
            CarList.journeys.Remove(j);
            Vehicle vehicle = CarList.vehicles.Find(v => v.Id == j.vehicleID);
            Booking booking = CarList.bookings.Find(b => b.id == BookingID);
            List<Journey> journeys = CarList.journeys.FindAll(journey => journey.BookingID == BookingID);
            JourneysListView.ItemsSource = journeys;
            journeysListChanged = true;
            booking.updateRentPrice(journeys);
            vehicle.updateTotalRentCost(CarList.bookings.FindAll(b => b.Vehicleid == vehicle.Id));
            j.SaveJourney(CarList.journeys);
            vehicle.SaveVehicles(CarList.vehicles);
            vehicleListView.ItemsSource = CarList.vehicles;
            vehicleListView.Items.Refresh();
        }
    }
}
