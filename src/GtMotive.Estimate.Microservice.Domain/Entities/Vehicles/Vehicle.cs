using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities.Vehicles
{
    /// <summary>
    /// Represents a vehicle entity with properties such as Id, LicensePlate, Model, ManufactureDate, and IsAvailable.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle.</param>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="brand">The brand of the vehicle.</param>
        /// <param name="manufactureDate">The manufacture date of the vehicle.</param>
        public Vehicle(LicensePlate licensePlate, string brand, string model, ManufactureDate manufactureDate)
        {
            Id = new VehicleId(Guid.NewGuid());
            LicensePlate = licensePlate ?? throw new ArgumentNullException(nameof(licensePlate));
            Model = model;
            Brand = brand;
            ManufactureDate = manufactureDate ?? throw new ArgumentNullException(nameof(manufactureDate));
            IsAvailable = true;
        }

        /// <summary>
        /// Gets the unique identifier for the vehicle.
        /// </summary>
        public VehicleId Id { get; private set; }

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        public LicensePlate LicensePlate { get; private set; }

        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        public string Brand { get; private set; }

        /// <summary>
        /// Gets the manufacture date of the vehicle.
        /// </summary>
        public ManufactureDate ManufactureDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the vehicle is available.
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Marks the vehicle as rented.
        /// </summary>
        /// <exception cref="DomainException">Thrown when the vehicle is already rented.</exception>
        public void Rent()
        {
            if (!IsAvailable) throw new DomainException("Vehicle is already rented.");
            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as returned.
        /// </summary>
        /// <exception cref="DomainException">Thrown when the vehicle is already available.</exception>
        public void Return()
        {
            if (IsAvailable) throw new DomainException("Vehicle is already available.");
            IsAvailable = true;
        }
    }
}
