using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Ports;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Aggregates;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    public class ReturnVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IReservationRepository _reservationRepository;
        private IReturnVehicleOutputPort _outputPort;

        public ReturnVehicleUseCase(IVehicleRepository vehicleRepository, IReservationRepository reservationRepository, IReturnVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public void SetOutputPort(IReturnVehicleOutputPort outputPort)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public async Task Execute(ReturnVehicleInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var reservation = await _reservationRepository.GetById(input.ReservationId)
                ?? throw new DomainException($"Reservation {input.ReservationId} not found");

            if (reservation.Status != ReservationStatus.Active)
            {
                throw new DomainException("The status reservation is not active");
            }

            var vehicleReturned = await _vehicleRepository.ReturnVehicleAsync(
                new VehicleId(reservation.VehicleId));

            if (!vehicleReturned)
            {
                throw new DomainException($"Vehicle {reservation.VehicleId} is not avaible to return");
            }

            reservation.MarkAsReturned(DateTime.UtcNow);
            await _reservationRepository.Update(reservation);

            _outputPort.StandardHandle();
        }
    }
}
