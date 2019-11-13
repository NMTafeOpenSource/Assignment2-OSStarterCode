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
    /// Interaction logic for ViewJourneys.xaml
    /// </summary>
    public partial class ViewJourneys : Window
    {
        public CarList carlist;
        public BookingList bookingList;
        public List<Journey> journeys;
        public ListView journeysListView;
        public Guid BookingID;

        public ViewJourneys()
        {
            InitializeComponent();
        }

    }
}
