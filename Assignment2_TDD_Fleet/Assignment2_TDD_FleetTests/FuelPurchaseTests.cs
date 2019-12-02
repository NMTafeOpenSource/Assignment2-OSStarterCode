using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2_TDD_Fleet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet.Tests
{
    [TestClass()]
    public class FuelPurchaseTests
    {
        [TestMethod()]
        public void FuelPurchaseTest()
        {
            // this is just a sample data/input
            double fuelQuantity = 30;
            double fuelPrice = 1.5;
            FuelPurchase fuelPurchase = new FuelPurchase(fuelQuantity, fuelPrice);
            // act
            fuelPurchase.TotalCost = fuelPrice * fuelQuantity;
            // assert
            Assert.AreEqual(45, fuelPurchase.TotalCost);
        }

        [TestMethod()]
        public void getFuelEconomyTest()
        {
            // this is just a sample data for vehicle
            Vehicle vehicle = new Vehicle();
            vehicle.Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            vehicle.RegistrationID = "21HSK1";
            vehicle.CarManufacture = "Ford";
            vehicle.CarModel = "LOL";
            vehicle.CarYear = 2019;
            vehicle.FuelType = "Petrol";
            vehicle.TankCapacity = 50;
            vehicle.VehicleOdometer = 100;
            // add to the new vehicle lists
            CarList.vehicles.Add(vehicle);

            Journey journey = new Journey();
            // this is just a sample data for journey
            journey.id = Guid.NewGuid();
            journey.BookingID = Guid.NewGuid();
            journey.vehicleID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            journey.JourneyStartAt = DateTime.Parse("2019-11-25");
            journey.JourneyEndedAt = DateTime.Parse("2019-11-26");
            journey.StartOdometer = 100;
            journey.EndOdometer = 460;
            journey.JourneyFrom = "Perth";
            journey.JourneyTo = "Midlands";
            CarList.journeys.Add(journey);

            // this is just a sample data for fuel purchases
            double FuelQuantity = double.Parse("30");
            double FuelPrice = double.Parse("1.5");
            FuelPurchase fuelPurchase = new FuelPurchase(FuelQuantity, FuelPrice);
            fuelPurchase.VId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");

            CarList.fuelPurchases.Add(fuelPurchase);

            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == fuelPurchase.VId);
            Journey relatedJourneys = CarList.journeys.Find(j => j.vehicleID == fuelPurchase.VId);
            List<FuelPurchase> fuelPurchases = CarList.fuelPurchases.Where(fp => fp.VId == relatedJourneys.vehicleID).ToList();
            List<Journey> allJourneysRelatedWithFps = CarList.journeys.Where(j => j.vehicleID == relatedVehicle.Id).ToList();

            double totalKmTravelled = 0.0;
            double totalFuelUsed = 0.0;

            if (fuelPurchases != null)
            {
                totalFuelUsed = fuelPurchases.Sum(fps => fps.FuelQuantity);
                totalKmTravelled = allJourneysRelatedWithFps.Max(max => max.EndOdometer) - allJourneysRelatedWithFps.Min(min => min.StartOdometer);
            }
            // act
            double fuelEconomy =  totalKmTravelled / totalFuelUsed;
            // assert
            // 360/30 expected result should be 12 
            Assert.AreEqual(12, fuelEconomy);
        }
    }
}