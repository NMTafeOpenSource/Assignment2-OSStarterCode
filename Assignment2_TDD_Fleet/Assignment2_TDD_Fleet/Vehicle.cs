﻿using Newtonsoft.Json;
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

        private FuelPurchase fuelPurchase;

        /**
         * Class constructor specifying name of make (manufacturer), model and year
         * of make.
         * @param manufacturer
         * @param model
         * @param makeYear
         */

        public Vehicle(string manufacture, string model, int makeYear)
        {
            this.CarManufacture = manufacture;
            this.CarModel = model;
            this.CarYear = makeYear;
            fuelPurchase = new FuelPurchase();
        }
        /**
         * Prints details for {@link Vehicle}
         */
        /*
       public void printDetails()
       {
           Console.WriteLine("Vehicle: " + CarYear + " " + CarManufacture + " " + CarModel);
           // TODO Display additional information about this vehicle
       }
       */

        // TODO Create an addKilometers method which takes a parameter for distance travelled 
        // and adds it to the odometer reading. 

        // adds fuel to the car
        /*
        public void addFuel(double litres, double price)
        {
            fuelPurchase.purchaseFuel(litres, price);
        }
        */
    }
}
