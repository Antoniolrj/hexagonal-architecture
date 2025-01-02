using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs
{
    public class RentVehicleOutput : IUseCaseOutput
    {
        public Guid ReservationId { get; set; }
    }
}
