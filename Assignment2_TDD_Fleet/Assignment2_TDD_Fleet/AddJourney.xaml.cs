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

        /// <summary>
        /// this is constructor for addJourney Window
        /// </summary>
        public AddJourney()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this is constructor for addJourney
        /// to get items from bookingList.cs
        /// </summary>
        /// <param name="startRentDate"></param>
        /// <param name="endRentDate"></param>
        /// <param name="startOdometer"></param>
        public AddJourney(DateTime startRentDate, DateTime endRentDate, int startOdometer)
        {
            InitializeComponent();
            // to set the label on addJourney Start date
            LabelStartAtDate.Content = "Your Rent Start At:" + " " + startRentDate.ToString();
            // to set the label on addJourney End date
            LabelEndedAtDate.Content = "Your Rent Ended At:" + " " + endRentDate.ToString();
        }
        /// <summary>
        /// this is constructor to get specific items from bookingList.cs
        /// in order to use it on this window
        /// </summary>
        /// <param name="startRentDate"></param>
        /// <param name="endRentDate"></param>
        /// <param name="startOdometer"></param>
        /// <param name="id"></param>
        /// <param name="vehicleId"></param>
        public AddJourney(DateTime startRentDate, DateTime endRentDate, int startOdometer, Guid id, Guid vehicleId) : this(startRentDate, endRentDate, startOdometer)
        {
            this.BookingID = id;
            this.vehicleId = vehicleId;
            InitializeComponent();
            journeys = new Journey();
        }
        /// <summary>
        /// this is a click event for add journey button
        /// and it has validation for the dates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // check if the start and end date valid
            if (isJourneyStartDateValid && isJourneyEndDateValid)
            {
                journeys.journeysListChanged = true;
                // if its a new journey then it will add new journey to a list
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
                    
                    CarList.journeys.Add(journey); // add new journey into journey's list on carList window
                    
                    Booking associatedBooking = CarList.bookings.Find(b => b.id == journey.BookingID); // finding associatedBooking that has the same booking Id
                    
                    Vehicle associatedVehicle = CarList.vehicles.Find(v => v.Id == journey.vehicleID); // finding associatedVehicle that has the same vehicleID
                    
                    List<Journey> associatedJourneys = CarList.journeys.FindAll(j => j.BookingID == journey.BookingID); // finding list of associatedJourneys by using journey guid
                   
                    List<Booking> bookings = CarList.bookings.FindAll(b => b.Vehicleid == associatedVehicle.Id);  // finding list of bookings via associated vehicle
                    
                    associatedBooking.updateRentPrice(associatedJourneys); // updates rent price whenever it adds journey
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
                // save the new journey into journey list on carList window
                journeys.SaveJourney(CarList.journeys);
                // close the addJourney Window
                Close();
            }
            else 
            // if the start and end date is null so it will show error message
            {
                LabelStartAtDate.Content = "You must put proper start booking date!";
                LabelEndedAtDate.Content = "You must put proper end booking date!";
            }
        }
        /// <summary>
        /// this is a method that i used to check
        /// what is the owner of this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddJourney_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Owner Window: {this.Owner}");
        }
        /// <summary>
        /// this is validation for journeyStartDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// this is validation for journeyEndDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
