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
    public class ViewJourneysTests
    {
        [TestMethod()]
        public void deleteJourneyTest()
        {
            CarList car = new CarList();
            Journey journey = new Journey();
            // this is just a sample data
            journey.id = Guid.NewGuid();
            journey.BookingID = Guid.NewGuid();
            journey.vehicleID = Guid.NewGuid();
            journey.JourneyStartAt = DateTime.Parse("2019-11-27");
            journey.JourneyEndedAt = DateTime.Parse("2019-11-28");
            journey.StartOdometer = 100;
            journey.EndOdometer = 126;
            journey.JourneyFrom = "Perth";
            journey.JourneyTo = "Midlands";

            CarList.journeys.Add(journey);
            // check if its deleted or not
            Assert.IsTrue(ViewJourneys.deleteJourney(journey));
        }
    }
}