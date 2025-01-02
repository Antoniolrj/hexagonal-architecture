using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Aggregates;

namespace GtMotive.Estimate.Microservice.Domain.Repositories
{
    /// <summary>
    /// Interface for reservation repository operations.
    /// </summary>
    public interface IReservationRepository
    {
        /// <summary>
        /// Adds a new reservation to the repository.
        /// </summary>
        /// <param name="reservation">The reservation to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Add(Reservation reservation);

        /// <summary>
        /// Retrieves a reservation for a specific client with an active status.
        /// </summary>
        /// <param name="customerId">The unique identifier of the client.</param>
        /// <returns>The active reservation for the client, or null if none exists.</returns>
        Task<Reservation> GetByCustomerId(Guid customerId);

        /// <summary>
        /// Retrieves all active reservations.
        /// </summary>
        /// <returns>A list of active reservations.</returns>
        Task<IReadOnlyCollection<Reservation>> GetActiveReservations();

        /// <summary>
        /// Retrieves a reservation by its unique identifier.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>The reservation, or null if not found.</returns>
        Task<Reservation> GetById(Guid reservationId);

        /// <summary>
        /// Updates the reservation in the repository.
        /// </summary>
        /// <param name="reservation">The reservation to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Update(Reservation reservation);
    }
}
