// REMOVE any "using" statements, which start with "Travel." BEFORE SUBMITTING
using NUnit.Framework;
using System.Text;
//using Travel.Core.Controllers;
//using Travel.Entities;
//using Travel.Entities.Airplanes;
//using Travel.Entities.Airplanes.Contracts;
//using Travel.Entities.Contracts;
//using Travel.Entities.Items;

namespace Travel.UserTests
{
    [TestFixture]
    public class FlightControllerTests
    {
        private FlightController flightController;
        private IAirport airport;
        private IAirplane airplane;

        [SetUp]
        public void TestInit()
        {
            this.airport = new Airport();
            this.flightController = new FlightController(airport);
            this.airplane = new LightAirplane();
        }

        [Test]
        public void ValidateTakeOffMethod()
        {
            var passengers = new Passenger[]
            {
                new Passenger("Pesho"),
                new Passenger("Gosho"),
                new Passenger("Ayse"),
                new Passenger("Ivancho"),
                new Passenger("Penka"),
                new Passenger("Ivanka")
            };

            var items = new Item[]
            {
               new Jewelery(),
               new Toothbrush(),
               new Laptop()
            };

            passengers[1].Bags.Add(new Bag(passengers[1], items));

            foreach (var passenger in passengers)
            {
                this.airplane.AddPassenger(passenger);
            }

            var trip = new Trip("Sofia", "Plovdiv", airplane);
            var tripToSkip = new Trip("Source", "Destination", airplane);
            tripToSkip.Complete();

            this.airport.AddTrip(trip);
            this.airport.AddTrip(tripToSkip);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SofiaPlovdiv1:");
            sb.AppendLine("Overbooked! Ejected Gosho");
            sb.AppendLine("Confiscated 1 bags ($3303)");
            sb.AppendLine("Successfully transported 5 passengers from Sofia to Plovdiv.");
            sb.Append("Confiscated bags: 1 (3 items) => $3303");

            Assert.That(flightController.TakeOff(), Is.EqualTo(sb.ToString()));
            foreach (var currentTrip in this.airport.Trips)
            {
                Assert.IsTrue(currentTrip.IsCompleted);
            }

        }
    }
}
