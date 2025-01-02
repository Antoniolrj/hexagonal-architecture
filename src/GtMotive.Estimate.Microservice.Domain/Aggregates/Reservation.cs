using System;

namespace GtMotive.Estimate.Microservice.Domain.Aggregates
{
    /// <summary>
    /// Represents a reservation entity with properties such as Id, ClientId, VehicleId, Status, ReservedAt, and ReturnedAt.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Gets the reservation identifier.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Gets the reservation status.
        /// </summary>
        public ReservationStatus Status { get; private set; }

        /// <summary>
        /// Gets the date and time when the reservation was made.
        /// </summary>
        public DateTime ReservedAt { get; private set; }

        /// <summary>
        /// Gets the date and time when the vehicle was returned.
        /// </summary>
        public DateTime? ReturnedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reservation"/> class.
        /// </summary>
        /// <param name="id">The reservation identifier.</param>
        /// <param name="customerId">The client identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="reservedAt">The date and time when the reservation was made.</param>
        public Reservation(Guid customerId, Guid vehicleId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            VehicleId = vehicleId;
            ReservedAt = DateTime.UtcNow;
            Status = ReservationStatus.Active;
        }

        /// <summary>
        /// Marks the reservation as returned.
        /// </summary>
        /// <param name="returnedAt">The date and time when the vehicle was returned.</param>
        public void MarkAsReturned(DateTime returnedAt)
        {
            Status = ReservationStatus.Returned;
            ReturnedAt = returnedAt;
        }
    }

    /// <summary>
    /// Represents the status of a reservation.
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// The reservation is active.
        /// </summary>
        Active,

        /// <summary>
        /// The reservation has been returned.
        /// </summary>
        Returned
    }
}
