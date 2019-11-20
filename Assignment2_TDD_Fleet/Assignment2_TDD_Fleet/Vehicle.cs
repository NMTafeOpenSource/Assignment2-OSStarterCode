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
        public Guid Id { get; set; }
        public string RegistrationID { get; set; }
        public string CarManufacture { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public string FuelType { get; set; }
        public double TankCapacity { get; set; }
        public int VehicleOdometer { get; set; }
        public double TotalRentalCost { get; set; }
        public int serviceCount { get; set; }

        public Vehicle()
        {

        }

        [JsonIgnore]
        public bool vehicleListChanged = false; // this is updated if you edit/add/delete the vehicles list
        public void SaveVehicles(List<Vehicle> vehicles)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("Vehicles.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, vehicles);
            }
            vehicleListChanged = false;
        }

        public void updateTotalRentCost(List<Booking> vehicleBookings)
        {
            double totalCost = 0.0;
            vehicleBookings.ForEach(b =>
            {
                totalCost += b.RentPrice;
            });
            this.TotalRentalCost = totalCost;
            SaveVehicles(CarList.vehicles);
        }

        public void updateServicesCount(List<Service> servicesList)
        {
            List<Service> servicesUpToToday = servicesList.Where(s => DateTime.Compare(s.ServiceDate, DateTime.Now) <= 0).ToList<Service>();
            this.serviceCount = servicesUpToToday.Count;
            this.SaveVehicles(CarList.vehicles);
        }


        public string printDetails()
        {
            List<Service> vehicleServices = CarList.services != null && CarList.services.Count > 0 ? CarList.services.FindAll(s => s.vehicleID == this.Id) : new List<Service>();
            Service latestService = vehicleServices != null && vehicleServices.Count > 0 ? Service.getLatestService(vehicleServices) : null;
            double kmSinceLastService = latestService != null ? this.VehicleOdometer - latestService.ServiceOdometer : 0;
            bool requiresService = vehicleServices != null && vehicleServices.Count > 0 ? Service.requiresService(vehicleServices) : false;
            string requiresServiceText = requiresService ? "Yes" : "No";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Vehicle: {this.CarManufacture} {this.CarModel} {this.CarYear}");
            sb.AppendLine($"Registration No: {this.RegistrationID}");
            sb.AppendLine($"Distance Travelled: {this.VehicleOdometer}");
            sb.AppendLine($"Total Services: {this.serviceCount}");
            sb.AppendLine($"Car Revenue: {this.TotalRentalCost.ToString("C")}");
            sb.AppendLine($"KM Since last service: {kmSinceLastService} km");
            sb.AppendLine($"Requires service: {requiresServiceText}");

            return sb.ToString();
        }

    }
}
