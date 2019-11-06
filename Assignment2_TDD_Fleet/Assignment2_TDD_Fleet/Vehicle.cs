using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Assignment2_TDD_Fleet
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationID { get; set; }
        public string CarManufacture { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string FuelType { get; set; }
        public string TankCapacity { get; set; }
        public string VehicleOdometer { get; set; }

        public Vehicle()
        {

        }

        public bool vehicleListChanged = false; // this is updated if you edit/add/delete the vehicles list
        public void SaveVehicles(List<Vehicle> vehicles)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("jsontestshit.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, vehicles);
            }
            vehicleListChanged = false;
        }
    }
}
