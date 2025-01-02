using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Ports;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Aggregates;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    public class RentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IReservationRepository _reservationRepository;
        private IRentVehicleOutputPort _outputPort;

        public RentVehicleUseCase(IVehicleRepository vehicleRepository, IReservationRepository reservationRepository, IRentVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public void SetOutputPort(IRentVehicleOutputPort outputPort)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public async Task Execute(RentVehicleInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var activeReservation = await _reservationRepository.GetByCustomerId(input.CustomerId);

            if (activeReservation != null && activeReservation.Status == ReservationStatus.Active)
            {
                throw new DomainException("El cliente ya tiene un vehículo alquilado.");
            }

            var vehicleRented = await _vehicleRepository.RentVehicleAsync(
                new VehicleId(input.VehicleId));

            if (!vehicleRented)
            {
                throw new DomainException($"Vehicle {input.VehicleId} is not avaible to rent");
            }

            var reservation = new Reservation(input.CustomerId, input.VehicleId);
            await _reservationRepository.Add(reservation);

            var output = new RentVehicleOutput
            {
                ReservationId = reservation.Id
            };

            _outputPort.StandardHandle(output);
        }
    }
}
