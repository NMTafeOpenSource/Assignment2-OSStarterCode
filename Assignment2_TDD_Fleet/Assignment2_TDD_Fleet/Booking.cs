using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class Booking
    {
        public string CustomerName { get; set; }
        public string SelectedVehicle { get; set; }
        public int StartOdometer { get; set; }
        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }
        public double RentPrice { get; set; }

        public Booking()
        {

        }

        public void SaveBookings(List<Booking> bookings)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("Bookings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, bookings);
            }
            //vehicleListChanged = false;
        }
    }
}
