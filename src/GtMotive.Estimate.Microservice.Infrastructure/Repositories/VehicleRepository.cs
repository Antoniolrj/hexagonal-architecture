using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;

        public VehicleRepository(MongoService mongoService)
        {
            _vehicles = mongoService.GetCollection<Vehicle>("Vehicles");
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _vehicles.InsertOneAsync(vehicle);
        }

        public async Task<IReadOnlyList<Vehicle>> GetAllAsync()
        {
            var vehicles = await _vehicles.Find(FilterDefinition<Vehicle>.Empty).ToListAsync();
            return vehicles.AsReadOnly();
        }

        public async Task<IReadOnlyList<Vehicle>> GetAvailableVehiclesAsync()
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.IsAvailable, true);
            var vehicles = await _vehicles.Find(filter).ToListAsync();
            return vehicles.AsReadOnly();
        }

        public async Task<Vehicle> GetByIdAsync(VehicleId vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId);
            return await _vehicles.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> RentVehicleAsync(VehicleId vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId) &
                         Builders<Vehicle>.Filter.Eq(v => v.IsAvailable, true);

            var update = Builders<Vehicle>.Update
                .Set(v => v.IsAvailable, false);

            var result = await _vehicles.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> ReturnVehicleAsync(VehicleId vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId) &
                         Builders<Vehicle>.Filter.Eq(v => v.IsAvailable, false);

            var update = Builders<Vehicle>.Update
                .Set(v => v.IsAvailable, true);

            var result = await _vehicles.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
