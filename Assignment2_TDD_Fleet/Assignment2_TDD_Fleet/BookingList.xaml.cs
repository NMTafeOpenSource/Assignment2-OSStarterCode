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
        public CarList carList;
        public List<Booking> bookings;
        public List<Booking> bookingsFromJSONFile;
        public ListView bookingListView;
        public bool bookingListChanged;

        public BookingList()
        {
            InitializeComponent();
            bookingListView = BookingsListView;
            // BookingsListView.ItemsSource = bookings;
            // CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(BookingsListView.ItemsSource);
            // bookings = new List<Booking>();
        }
    }
}
