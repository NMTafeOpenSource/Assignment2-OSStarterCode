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
    public class VehicleHistoryTests
    {
        [TestMethod()]
        public void deleteServiceTest()
        {
            CarList car = new CarList();
            Service service = new Service();
            // this is just a sample data
            service.vehicleID = Guid.NewGuid();
            service.ServiceOdometer = int.Parse("100");
            service.ServiceDate = DateTime.Now;
            // add to service list
            CarList.services.Add(service);
            // this is to check if its deleted or not
            // it will return true if it does
            Assert.IsTrue(VehicleHistory.deleteService(service));
        }

        [TestMethod()]
        public void deleteFuelPurchasesTest()
        {
            CarList car = new CarList();
            // this is just a sample data
            double FuelQuantity = double.Parse("30");
            double FuelPrice = double.Parse("1.5");
            FuelPurchase fuelPurchase = new FuelPurchase(FuelQuantity, FuelPrice);
            fuelPurchase.VId = Guid.NewGuid();

            CarList.fuelPurchases.Add(fuelPurchase);
            // it will return true if its deleted
            Assert.IsTrue(VehicleHistory.deleteFuelPurchases(fuelPurchase));
        }
    }
}