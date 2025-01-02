using System.Collections.Generic;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Entities.Vehicles
{
    /// <summary>
    /// Interface for vehicle repository operations.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Retrieves all vehicles from the repository.
        /// </summary>
        /// <returns>A read-only list of all vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAllAsync();

        /// <summary>
        /// Retrieves available vehicles (not rented) from the repository.
        /// </summary>
        /// <returns>A list of vehicles that are currently available for rent.</returns>
        Task<IReadOnlyList<Vehicle>> GetAvailableVehiclesAsync();

        /// <summary>
        /// Retrieves a vehicle by its ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to retrieve.</param>
        /// <returns>The vehicle with the specified ID, or null if no such vehicle exists.</returns>
        Task<Vehicle> GetByIdAsync(VehicleId vehicleId);

        /// <summary>
        /// Rents a vehicle to a customer.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to rent.</param>
        /// <returns>True if the operation succeeds; otherwise, false.</returns>
        Task<bool> RentVehicleAsync(VehicleId vehicleId);

        /// <summary>
        /// Returns a rented vehicle back to the fleet.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to return.</param>
        /// <returns>True if the operation succeeds; otherwise, false.</returns>
        Task<bool> ReturnVehicleAsync(VehicleId vehicleId);
    }
}
