using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class Journey
    {
        public Guid id { get; set; }
        public Guid BookingID { get; set; }
        public DateTime JourneyStartAt { get; set; }
        public DateTime JourneyEndedAt { get; set; }
        public int StartOdometer { get; set; }
        public int EndOdometer { get; set; }
        public string JourneyFrom { get; set; }
        public string JourneyTo { get; set; }

        private double kilometers;

        /**
         * Class constructor
         */
        public Journey()
        {
            this.kilometers = 0;
        }

        /** 
         * Appends the distance parameter to {@link #kilometers}
         * @param kilometers the distance traveled 
         */
        public void addKilometers(double kilometers)
        {
            this.kilometers += kilometers;
        }

        /**
         * Getter method for total Kilometers traveled in this journey.
         * @return {@link #kilometers}
         */
        public double getKilometers()
        {
            return kilometers;
        }

        public void SaveJourney(List<Journey> journeys)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("Journey.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, journeys);
            }
            //vehicleListChanged = false;
        }
    }
}
