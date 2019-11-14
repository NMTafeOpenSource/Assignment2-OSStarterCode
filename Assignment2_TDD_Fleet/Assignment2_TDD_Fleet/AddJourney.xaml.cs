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
    /// Interaction logic for AddJourney.xaml
    /// </summary>
    public partial class AddJourney : Window
    {
        public CarList carList;
        public static Journey journeys;
        public static Booking bookings;
        public static bool newJourney = true;
        //public Guid id;
        public Guid BookingID;
        //public Guid vehicleID;

        public AddJourney()
        {
            InitializeComponent();
        }

        public AddJourney(DateTime startRentDate, DateTime endRentDate, int startOdometer)
        {
            InitializeComponent();
            LabelStartAtDate.Content = "Your Rent Start At:" + " " + startRentDate.ToString();
            LabelEndedAtDate.Content = "Your Rent Ended At:" + " " + endRentDate.ToString();
            StartOdometerJourneyTextBox.Text = startOdometer.ToString();
        }

        public AddJourney(DateTime startRentDate, DateTime endRentDate, int startOdometer, Guid id) : this(startRentDate, endRentDate, startOdometer)
        {
            this.BookingID = id;
            //this.vehicleID = id;
            InitializeComponent();
            journeys = new Journey();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (newJourney)
            {
                Journey journey = new Journey();
                journey.id = Guid.NewGuid();
                journey.BookingID = BookingID;
                //journey.vehicleID = vehicleID;
                journey.JourneyStartAt = DateTime.Parse(JourneyStartAtDate.Text);
                journey.JourneyEndedAt = DateTime.Parse(JourneyEndedAtDate.Text);
                journey.StartOdometer = int.Parse(StartOdometerJourneyTextBox.Text);
                journey.EndOdometer = int.Parse(EndedOdometerJourneyTextBox.Text);
                journey.JourneyFrom = JourneyFromTextBox.Text;
                journey.JourneyTo = JourneyToTextBox.Text;

                CarList.journeys.Add(journey);
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
    }
}
