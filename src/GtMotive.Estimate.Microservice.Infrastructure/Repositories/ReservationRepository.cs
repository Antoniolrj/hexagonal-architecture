using GtMotive.Estimate.Microservice.Domain.Aggregates;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationRepository(MongoService mongoService)
        {
            _reservations = mongoService.GetCollection<Reservation>("Reservations");
        }

        public async Task Add(Reservation reservation)
        {
            await _reservations.InsertOneAsync(reservation);
        }

        public async Task<Reservation> GetByCustomerId(Guid customerId)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.CustomerId, customerId) &
                         Builders<Reservation>.Filter.Eq(r => r.Status, ReservationStatus.Active);

            return await _reservations.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Reservation>> GetActiveReservations()
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Status, ReservationStatus.Active);
            var reservations = await _reservations.Find(filter).ToListAsync();
            return reservations.AsReadOnly();
        }

        public async Task<Reservation> GetById(Guid reservationId)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, reservationId);
            return await _reservations.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Update(Reservation reservation)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, reservation.Id);

            var update = Builders<Reservation>.Update
                .Set(r => r.Status, reservation.Status)
                .Set(r => r.ReturnedAt, reservation.ReturnedAt);

            await _reservations.UpdateOneAsync(filter, update);
        }
    }
}
