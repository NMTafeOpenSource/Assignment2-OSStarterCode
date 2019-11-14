﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class Booking
    {
        public Guid id { get; set; }
        public string CustomerName { get; set; }
        public string SelectedVehicle { get; set; }
        public string RentalType { get; set; }
        public int StartOdometer { get; set; }
        public int EndOdometer { get; set; }
        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }
        public double RentPrice { get; set; }

        public Booking()
        {

        }

        [JsonIgnore]
        public bool bookingListChanged = false;

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
            bookingListChanged = false;
        }
    }
}
