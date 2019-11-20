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
    /// Interaction logic for AddJourney.xaml
    /// </summary>
    public partial class AddJourney : Window
    {
        public static Journey journeys;
        public static Booking bookings;
        public static bool newJourney = true;
        //public Guid id;
        public Guid BookingID;
        public Guid vehicleId;
        private bool isJourneyStartDateValid = false;
        private bool isJourneyEndDateValid = false;

        public AddJourney()
        {
            InitializeComponent();
            // Debug.WriteLine($"Owner Window: {this.Owner}");
            // Console.WriteLine("hello???");
        }

        public AddJourney(DateTime startRentDate, DateTime endRentDate, int startOdometer)
        {
            InitializeComponent();
            LabelStartAtDate.Content = "Your Rent Start At:" + " " + startRentDate.ToString();
            LabelEndedAtDate.Content = "Your Rent Ended At:" + " " + endRentDate.ToString();
            //StartOdometerJourneyTextBox.Text = startOdometer.ToString();
        }

        public AddJourney(DateTime startRentDate, DateTime endRentDate, int startOdometer, Guid id, Guid vehicleId) : this(startRentDate, endRentDate, startOdometer)
        {
            this.BookingID = id;
            this.vehicleId = vehicleId;
            InitializeComponent();
            journeys = new Journey();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isJourneyStartDateValid && isJourneyEndDateValid)
            {
                journeys.journeysListChanged = true;
                if (newJourney)
                {
                    Journey journey = new Journey();
                    journey.id = Guid.NewGuid();
                    journey.BookingID = BookingID;
                    journey.vehicleID = vehicleId;
                    journey.JourneyStartAt = DateTime.Parse(JourneyStartAtDate.Text);
                    journey.JourneyEndedAt = DateTime.Parse(JourneyEndedAtDate.Text);
                    journey.StartOdometer = int.Parse(StartOdometerJourneyTextBox.Text);
                    journey.EndOdometer = int.Parse(EndedOdometerJourneyTextBox.Text);
                    journey.JourneyFrom = JourneyFromTextBox.Text;
                    journey.JourneyTo = JourneyToTextBox.Text;

                    CarList.journeys.Add(journey);

                    Booking associatedBooking = CarList.bookings.Find(b => b.id == journey.BookingID);
                    Vehicle associatedVehicle = CarList.vehicles.Find(v => v.Id == journey.vehicleID);
                    List<Journey> associatedJourneys = CarList.journeys.FindAll(j => j.BookingID == journey.BookingID);
                    List<Booking> bookings = CarList.bookings.FindAll(b => b.Vehicleid == associatedVehicle.Id);
                    associatedBooking.updateRentPrice(associatedJourneys);
                    associatedVehicle.updateTotalRentCost(bookings);

                    ((CarList)this.Owner).updateOdometer();

                }
                else
                {
                    journeys.JourneyStartAt = DateTime.Parse(JourneyStartAtDate.Text);
                    journeys.JourneyEndedAt = DateTime.Parse(JourneyEndedAtDate.Text);
                    journeys.StartOdometer = int.Parse(StartOdometerJourneyTextBox.Text);
                    journeys.EndOdometer = int.Parse(EndedOdometerJourneyTextBox.Text);
                    journeys.JourneyFrom = JourneyFromTextBox.Text;
                    journeys.JourneyTo = JourneyToTextBox.Text;
                }
                journeys.SaveJourney(CarList.journeys);
                Close();
            }
            else 
            {
                LabelStartAtDate.Content = "You must put start booking date!";
                LabelEndedAtDate.Content = "You must put end booking date!";
            }
        }

        private void AddJourney_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Owner Window: {this.Owner}");
        }

        private void JourneyStartAtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? journeyStartDate = JourneyStartAtDate.SelectedDate;
            if (journeyStartDate != null)
            {
                Booking relatedBooking = CarList.bookings.Find(b => b.id == BookingID);
                if (DateTime.Compare((DateTime)journeyStartDate, relatedBooking.StartRentDate) < 0)
                {
                    LabelStartAtDate.Content = "Journey start date cannot be earlier than booking start rent date";
                    isJourneyStartDateValid = false;
                }
                else if (DateTime.Compare((DateTime)journeyStartDate, relatedBooking.EndRentDate) > 0)
                {
                    LabelStartAtDate.Content = "Journey start date cannot be after booking end rent date";
                    isJourneyStartDateValid = false;
                }
                else
                {
                    LabelStartAtDate.Content = "OK";
                    isJourneyStartDateValid = true;
                }
            }
        }

        private void JourneyEndedAtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? journeyEndDate = JourneyEndedAtDate.SelectedDate;
            DateTime? journeyStartDate = JourneyStartAtDate.SelectedDate;
            if (journeyEndDate != null)
            {
                Booking relatedBooking = CarList.bookings.Find(b => b.id == BookingID);
                if (DateTime.Compare((DateTime)journeyEndDate, relatedBooking.StartRentDate) < 0)
                {
                    LabelEndedAtDate.Content = "Journey end date cannot be earlier than booking start rent date";
                    isJourneyEndDateValid = false;
                }
                else if (journeyStartDate != null && DateTime.Compare((DateTime)journeyStartDate, (DateTime)journeyEndDate) > 0)
                {
                    LabelEndedAtDate.Content = "Journey end date cannot be earlier than journey start date";
                    isJourneyEndDateValid = false;
                }
                else if (DateTime.Compare((DateTime)journeyEndDate, relatedBooking.EndRentDate) > 0)
                {
                    LabelEndedAtDate.Content = "Journey end date cannot be after rent end date";
                    isJourneyEndDateValid = false;
                }
                else
                {
                    LabelEndedAtDate.Content = "OK";
                    isJourneyEndDateValid = true;
                }
            }
        }
    }
}
