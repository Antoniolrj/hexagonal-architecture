using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs
{
    public class RegisterVehicleOutput : IUseCaseOutput
    {
        public Guid VehicleId { get; set; }
    }
}
