using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2_TDD_Fleet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet.Tests
{
    [TestClass()]
    public class BookingListTests
    {
        [TestMethod()]
        public void deleteBookingTest()
        {
            CarList carList = new CarList();
            Booking booking = new Booking();
            Guid id = new Guid();
            Guid vehicleId = new Guid();

            // this is just a sample data
            booking.id = id;
            booking.Vehicleid = vehicleId;
            booking.CustomerName = "Kopal";
            booking.StartOdometer = 100;
            booking.SelectedVehicle = "BMW Z4";
            booking.StartRentDate = DateTime.Now;
            booking.EndRentDate = DateTime.Now;
            booking.RentalType = (BookingType)Enum.Parse(typeof(BookingType), "Day");

            CarList.bookings.Add(booking);
            // check if its deleted or not by this method
            Assert.IsTrue(BookingList.deleteBooking(booking));
        }
    }
}