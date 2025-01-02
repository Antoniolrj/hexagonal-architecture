using System.Text.RegularExpressions;

namespace GtMotive.Estimate.Microservice.Domain.Entities.Vehicles
{
    /// <summary>
    /// Represents a vehicle's license plate.
    /// </summary>
    public class LicensePlate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> class.
        /// </summary>
        /// <param name="value">The manufacture date value.</param>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("License plate cannot be empty.");

            // Format validator for license plate in Spain.
            if (!Regex.IsMatch(value, "^[A-Z0-9-]+$"))
                throw new DomainException("License plate format is invalid.");

            Value = value.ToUpperInvariant();
        }

        /// <summary>
        /// Gets the value of the license plate.
        /// </summary>
        public string Value { get; }
    }
}
