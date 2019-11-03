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
        public string RegistrationID { get; set; }
        public string CarManufacture { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string FuelType { get; set; }
        public string TankCapacity { get; set; }
        public string VehicleOdometer { get; set; }


        public bool vehicleListChanged = false; // this is updated if you edit/add/delete the vehicles list

        private FuelPurchase fuelPurchase;

        /**
         * Class constructor specifying name of make (manufacturer), model and year
         * of make.
         * @param manufacturer
         * @param model
         * @param makeYear
         */
        public Vehicle()
        {
            
        }


        public Vehicle(string manufacture, string model, string makeYear)
        {
            this.CarManufacture = manufacture;
            this.CarModel = model;
            this.CarYear = makeYear;
            fuelPurchase = new FuelPurchase();
        }

        /**
         * Prints details for {@link Vehicle}
         */
        public void printDetails()
        {
            Console.WriteLine("Vehicle: " + CarYear + " " + CarManufacture + " " + CarModel);
            // TODO Display additional information about this vehicle
        }


        // TODO Create an addKilometers method which takes a parameter for distance travelled 
        // and adds it to the odometer reading. 

        // adds fuel to the car
        public void addFuel(double litres, double price)
        {
            fuelPurchase.purchaseFuel(litres, price);
        }

    }
}
