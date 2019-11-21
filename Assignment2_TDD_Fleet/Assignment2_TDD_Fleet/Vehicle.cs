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
        /// <summary>
        /// this is a method to save vehicles to json file
        /// </summary>
        /// <param name="vehicles"></param>
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
        /// <summary>
        /// this is a method to update the rental cost
        /// </summary>
        /// <param name="vehicleBookings"></param>
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
        /// <summary>
        /// this is a method to get the number of services
        /// </summary>
        /// <param name="servicesList"></param>
        public void updateServicesCount(List<Service> servicesList)
        {
            List<Service> servicesUpToToday = servicesList.Where(s => DateTime.Compare(s.ServiceDate, DateTime.Now) <= 0).ToList<Service>();
            this.serviceCount = servicesUpToToday.Count;
            this.SaveVehicles(CarList.vehicles);
        }
        /// <summary>
        /// this is a method to get distance travelled
        /// from registered start odometer upToNow
        /// </summary>
        /// <returns></returns>
        public double getTotalDistanceTravelled()
        {
            List<Journey> relatedJourneys = CarList.journeys.Where(j => j.vehicleID == Id).ToList();
            double totalDistanceTravelled = 0.0;
            if (relatedJourneys != null)
            {
                totalDistanceTravelled = relatedJourneys.Max(max => max.EndOdometer) - relatedJourneys.Min(min => min.StartOdometer);
            }
            return totalDistanceTravelled;
        }
        /// <summary>
        /// this is a method to show details for specific vehicle
        /// </summary>
        /// <returns></returns>
        public string printDetails()
        {
            List<Service> vehicleServices = CarList.services != null && CarList.services.Count > 0 ? CarList.services.FindAll(s => s.vehicleID == this.Id) : new List<Service>();
            Service latestService = vehicleServices != null && vehicleServices.Count > 0 ? Service.getLatestService(vehicleServices) : null;
            //double kmSinceLastService = latestService != null ? (this.VehicleOdometer - latestService.ServiceOdometer) : 0;
            double kmSinceLastService = vehicleServices != null ? this.VehicleOdometer - vehicleServices.Last(v => v.vehicleID == Id).ServiceOdometer : 0;
            Journey relatedJourneys = CarList.journeys.Find(j => j.vehicleID == Id);

            bool requiresService = vehicleServices != null && vehicleServices.Count > 0 ? Service.requiresService(vehicleServices) : false;
            string requiresServiceText = requiresService ? "Yes" : "No";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Vehicle: {this.CarManufacture} {this.CarModel} {this.CarYear}");
            sb.AppendLine($"Registration No: {this.RegistrationID}");
            double distanceTravelled = this.getTotalDistanceTravelled();
            if (distanceTravelled > 0)
            {
                sb.AppendLine($"Distance Travelled: {distanceTravelled} km");
            }
            else
            {
                sb.AppendLine($"This vehicle never used before for journeys");
            }
            
            int serviceCount = this.serviceCount;
            if (serviceCount > 0)
            {
                sb.AppendLine($"Total Services: {this.serviceCount}");
            }
            else
            {
                sb.AppendLine($"Total Services: no record");
            }
            sb.AppendLine($"Car Revenue: {this.TotalRentalCost.ToString("C")}");
            sb.AppendLine($"KM Since last service: {kmSinceLastService} km");
            double fuelEconomy = FuelPurchase.getFuelEconomy(this.Id);
            if (fuelEconomy > 0)
            {
                sb.AppendLine($"Fuel Economy: {fuelEconomy} km/L");
            }
            else
            {
                sb.AppendLine($"Fuel Economy: not available yet");
            }
            sb.AppendLine($"Requires service: {requiresServiceText}");

            return sb.ToString();
        }

    }
}
