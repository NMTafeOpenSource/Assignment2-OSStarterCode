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
    public class CarListTests
    {
        [TestMethod()]
        public void deleteVehicleTest()
        {
            CarList carList = new CarList();
            Vehicle vehicle = new Vehicle();
            Guid id = new Guid();

            // this is just a sample data
            vehicle.Id = id;
            vehicle.RegistrationID = "21HSK1";
            vehicle.CarManufacture = "Ford";
            vehicle.CarModel = "LOL";
            vehicle.CarYear = 2019;
            vehicle.FuelType = "Petrol";
            vehicle.TankCapacity = 20;
            vehicle.VehicleOdometer = 1000;

            CarList.vehicles.Add(vehicle);
            // check if its deleted or not
            Assert.IsTrue(CarList.deleteVehicle(vehicle));
        }

    }
}