using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class Service
    {
        public Guid vehicleID { get; set; }
        public int ServiceOdometer { get; set; }
        public DateTime ServiceDate { get; set; }

        [JsonIgnore]
        public bool servicesListChanged = false;
        /// <summary>
        /// this is a method to save services to json FIle
        /// </summary>
        /// <param name="services"></param>
        public static void SaveServices(List<Service> services)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("Service.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, services);
            }
            //servicesListChanged = false;
        }
        /// <summary>
        /// this is a method to find service uptoDay
        /// </summary>
        /// <param name="vehicleServices"></param>
        /// <returns></returns>
        public static Service getLatestService(List<Service> vehicleServices)
        {
            return vehicleServices.Find(s =>
            {
                return DateTime.Compare(s.ServiceDate.Date, DateTime.Now.Date) == 0;
            });
        }
        /// <summary>
        /// this is to check if the vehicle requires service or not
        /// </summary>
        /// <param name="vehicleServices"></param>
        /// <returns></returns>
        public static bool requiresService(List<Service> vehicleServices)
        {
            return vehicleServices.Any(s =>
            {
                return DateTime.Compare(s.ServiceDate.Date, DateTime.Now.Date) >= 0;
            });
        }

    }
}
