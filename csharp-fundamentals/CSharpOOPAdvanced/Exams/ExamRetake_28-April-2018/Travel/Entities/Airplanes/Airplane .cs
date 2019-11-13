namespace Travel.Entities.Airplanes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Entities.Contracts;
    using Travel.Entities.Airplanes.Contracts;

    // migrated from java. please do the needful kind sir
    public abstract class Airplane : IAirplane
    {
        private readonly List<IBag> baggageCompartment;
        private readonly List<IPassenger> passengers;

        protected Airplane(int seats, int baggage)
        {
            this.passengers = new List<IPassenger>();
            this.Seats = seats;
            this.BaggageCompartments = baggage;
            this.baggageCompartment = new List<IBag>();
        }

        public int Seats { get; }
        public int BaggageCompartments { get; }

        public IReadOnlyCollection<IBag> BaggageCompartment
        {
            get
            {
                return this.baggageCompartment.AsReadOnly();
            }
        }

        public IReadOnlyCollection<IPassenger> Passengers
        {
            get
            {
                return this.passengers.AsReadOnly();
            }
        }

        public bool IsOverbooked => this.Passengers.Count() > this.Seats;

        public void AddPassenger(IPassenger passenger)
        {
            this.passengers.Add(passenger);
        }

        public IPassenger RemovePassenger(int passengerIndex)
        {
            // mdrchd
            var passenger = this.passengers[passengerIndex];

            this.passengers.RemoveAt(passengerIndex);

            return passenger;
        }

        public IEnumerable<IBag> EjectPassengerBags(IPassenger passenger)
        {
            var passengerBags = this.baggageCompartment
                .Where(b => b.Owner == passenger)
                .ToList();

            foreach (var bag in passengerBags)
                this.baggageCompartment.Remove(bag);

            return passengerBags;
        }

        public void LoadBag(IBag bag)
        {
            var isBaggageCompartmentFull = this.BaggageCompartment.Count > this.BaggageCompartments;
            if (isBaggageCompartmentFull)
            {
                throw new InvalidOperationException($"No more bag room in {this.GetType().ToString()}!");
            }

            this.baggageCompartment.Add(bag);
        }
    }
}