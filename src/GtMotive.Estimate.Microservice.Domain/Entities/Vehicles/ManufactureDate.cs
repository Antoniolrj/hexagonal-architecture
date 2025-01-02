using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities.Vehicles
{
    /// <summary>
    /// Represents the manufacture date of a vehicle as a value object.
    /// Ensures the date is not in the future and does not exceed a 5-year limit.
    /// </summary>
    public class ManufactureDate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManufactureDate"/> class.
        /// </summary>
        /// <param name="value">The manufacture date value.</param>
        private ManufactureDate(DateTime value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the manufacture date value.
        /// </summary>
        public DateTime Value { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ManufactureDate"/> instance if the provided date is valid.
        /// </summary>
        /// <param name="value">The manufacture date value.</param>
        /// <returns>
        /// A new instance of <see cref="ManufactureDate"/> if valid; otherwise, throws an exception.
        /// </returns>
        public static ManufactureDate Create(DateTime value)
        {
            if (value > DateTime.UtcNow)
                throw new DomainException("Manufacture date cannot be in the future.");

            if (value <= DateTime.UtcNow.AddYears(-5))
                throw new DomainException("Vehicles older than 5 years are not allowed.");

            return new ManufactureDate(value);
        }
    }
}
