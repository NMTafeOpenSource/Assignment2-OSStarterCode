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
        

        private double fuelEconomy;
        private double litres = 0;
        private double cost = 0;

        public FuelPurchase(double fuelQuantity, double fuelPrice)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelPrice = fuelPrice;
            this.TotalCost = fuelPrice * fuelQuantity;
        }

        public double getFuelEconomy()
        {
            return fuelEconomy;
            //return this.cost / this.litres;
        }

        public double getFuel()
        {
            return this.litres;
        }

        public void setFuelEconomy(double fuelEconomy)
        {
            this.fuelEconomy = fuelEconomy;
        }
        public void purchaseFuel(double amount, double price)
        {
            this.litres += amount;
            this.cost += price;
        }
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
