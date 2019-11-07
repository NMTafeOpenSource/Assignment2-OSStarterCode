using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for BookingList.xaml
    /// </summary>
    public partial class BookingList : Window
    {
        public static List<Booking> bookings;
        internal ListView bookingListView;
        public Booking booking;
        string bookingFileName = "Bookings.json";

        public BookingList()
        {
            InitializeComponent();
            bookingListView = BookingsListView;
            BookingsListView.ItemsSource = bookings;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(BookingsListView.ItemsSource);
            bookings = new List<Booking>();
            LoadBooking();
        }

        public void LoadBooking()
        {
            bookings.Clear();
            // deserialize JSON directly from a file
            bookings = (List<Booking>)JsonConvert.DeserializeObject(File.ReadAllText(bookingFileName), typeof(List<Booking>));
            BookingsListView.ItemsSource = bookings;
            BookingsListView.Items.Refresh();
        }

    }
}
