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
    public class ServiceTests
    {
        [TestMethod()]
        public void getLatestServiceTest()
        {
            // this is just a sample data
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
            // sample data for service
            Service service = new Service();
            service.vehicleID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            service.ServiceOdometer = int.Parse("100");
            service.ServiceDate = DateTime.Parse("2019-11-20");
            // add to service list
            CarList.services.Add(service);
            // i added another sample data for service
            service.vehicleID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            service.ServiceOdometer = int.Parse("200");
            service.ServiceDate = DateTime.Parse("2019-11-26");
            // add to service list
            CarList.services.Add(service);
            // i added another sample data for service
            service.vehicleID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            service.ServiceOdometer = int.Parse("300");
            service.ServiceDate = DateTime.Parse("2019-11-28");
            CarList.services.Add(service);

            List<Service> vehicleService = CarList.services.Where(s => s.vehicleID == vehicle.Id).ToList();
            // act
            Service getLatestService = vehicleService.Find(s =>
            {
                return DateTime.Compare(s.ServiceDate.Date, DateTime.Now.Date) == 0;
            });
            // expected date
            DateTime expected = DateTime.Parse("2019-11-28");
            // assert
            Assert.AreEqual(expected, getLatestService.ServiceDate);
        }
    }
}