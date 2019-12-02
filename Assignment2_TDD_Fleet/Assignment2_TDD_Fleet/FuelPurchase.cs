using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class FuelPurchase
    {
        public Guid VId { get; set; }
        public double FuelQuantity { get; set; }
        public double FuelPrice { get; set; }
        public double TotalCost { get; set; }
        
        /// <summary>
        /// this is a constructor for this window and
        /// getting fuelquantity and fuel purchase
        /// from fuel pucrhcases window
        /// </summary>
        /// <param name="fuelQuantity"></param>
        /// <param name="fuelPrice"></param>
        public FuelPurchase(double fuelQuantity, double fuelPrice)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelPrice = fuelPrice;
            this.TotalCost = fuelPrice * fuelQuantity;
        }
        /// <summary>
        /// this is a method to get the fuel economy by
        /// seacrhing totalKmtravelled divided by total fuel
        /// </summary>
        /// <param name="VId"></param>
        /// <returns></returns>
        public static double getFuelEconomy(Guid VId)
        {
            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == VId);
            Journey relatedJourneys = CarList.journeys.Find(j => j.vehicleID == VId);
            List<FuelPurchase> fuelPurchases = CarList.fuelPurchases.Where(fp => fp.VId == relatedJourneys.vehicleID).ToList();
            List<Journey> allJourneysRelatedWithFps = CarList.journeys.Where(j => j.vehicleID == relatedVehicle.Id).ToList();

            double totalKmTravelled = 0.0;
            double totalFuelUsed = 0.0;

            if (fuelPurchases != null)
            {
                totalFuelUsed = fuelPurchases.Sum(fps => fps.FuelQuantity);
                totalKmTravelled = allJourneysRelatedWithFps.Max(max => max.EndOdometer) - allJourneysRelatedWithFps.Min(min => min.StartOdometer);
            }
            return totalKmTravelled/totalFuelUsed;
        }

        /// <summary>
        /// this is a method to save fuel purchases
        /// </summary>
        /// <param name="fuelPurchases"></param>
        public static void SaveFuelPurchases(List<FuelPurchase> fuelPurchases)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("FuelPurchases.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, fuelPurchases);
            }
        }
    }
}
