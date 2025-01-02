using System.Collections.Generic;
using System.Collections.ObjectModel;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Entities.Customer
{
    /// <summary>
    /// Represents a customer in the system.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the customer.</param>
        /// <param name="name">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        public Customer(CustomerId id, string name, string lastName)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            RentVehicles = new ReadOnlyCollection<Vehicle>(new List<Vehicle>());
        }

        /// <summary>
        /// Gets the unique identifier for the customer.
        /// </summary>
        public CustomerId Id { get; }

        /// <summary>
        /// Gets the first name of the customer.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets last name of the customer.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the full name of the customer.
        /// </summary>
        public string FullName => $"{Name} {LastName}";

        /// <summary>
        /// Gets the collection of vehicles rented by the customer.
        /// </summary>
        public ReadOnlyCollection<Vehicle> RentVehicles { get; private set; }
    }
}
