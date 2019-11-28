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
    public class VehicleTests
    {
        [TestMethod()]
        public void getTotalDistanceTravelledTest()
        {
            Vehicle vehicle = new Vehicle();

            // this is just a sample data for vehicle
            vehicle.Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            vehicle.RegistrationID = "21HSK1";
            vehicle.CarManufacture = "Ford";
            vehicle.CarModel = "LOL";
            vehicle.CarYear = 2019;
            vehicle.FuelType = "Petrol";
            vehicle.TankCapacity = 50;
            vehicle.VehicleOdometer = 100;

            CarList.vehicles.Add(vehicle);

            Journey journey = new Journey();
            // this is just a sample data for journey
            journey.id = Guid.NewGuid();
            journey.BookingID = Guid.NewGuid();
            // i parsed the same id with vehicle id
            journey.vehicleID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            journey.JourneyStartAt = DateTime.Parse("2019-11-27");
            journey.JourneyEndedAt = DateTime.Parse("2019-11-28");
            journey.StartOdometer = 100;
            journey.EndOdometer = 126;
            journey.JourneyFrom = "Perth";
            journey.JourneyTo = "Midlands";

            CarList.journeys.Add(journey);
            List<Journey> relatedJourneys = CarList.journeys.Where(j => j.vehicleID == vehicle.Id).ToList();
            double totalDistanceTravelled = 0.0;
            if (relatedJourneys != null)
            {
                // act
                totalDistanceTravelled = relatedJourneys.Max(max => max.EndOdometer) - relatedJourneys.Min(min => min.StartOdometer);
            }
            // assert
            // 126 - 100 is 26 so expected result is 26
            Assert.AreEqual(26, totalDistanceTravelled);
        }

        [TestMethod()]
        public void updateServicesCountTest()
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
            List<Service> associatedService = CarList.services.Where(s => s.vehicleID == vehicle.Id).ToList();
            List<Service> servicesUpToToday = associatedService.Where(s => DateTime.Compare(s.ServiceDate, DateTime.Now) <= 0).ToList<Service>();
            // act
            vehicle.serviceCount = servicesUpToToday.Count;

            // assert
            // expected result should be 2 services
            Assert.AreEqual(2, vehicle.serviceCount);
        }
    }
}