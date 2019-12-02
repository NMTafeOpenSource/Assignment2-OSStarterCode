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
        public Guid vehicleID { get; set; }
        public DateTime JourneyStartAt { get; set; }
        public DateTime JourneyEndedAt { get; set; }
        public int StartOdometer { get; set; }
        public int EndOdometer { get; set; }
        public string JourneyFrom { get; set; }
        public string JourneyTo { get; set; }


        [JsonIgnore]
        public bool journeysListChanged = false;
        /// <summary>
        /// this is a save method for journey to json file
        /// </summary>
        /// <param name="journeys"></param>
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
            journeysListChanged = false;
        }
        /// <summary>
        /// this is a method return dateTime and Int into string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Journey start date: {this.JourneyStartAt}, Journey end date: {this.JourneyEndedAt}, journey end odometer: {this.EndOdometer}"; ;
        }
    }
}
