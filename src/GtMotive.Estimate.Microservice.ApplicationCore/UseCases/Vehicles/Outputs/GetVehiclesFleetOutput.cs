using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs
{
    public class GetVehiclesFleetOutput : IUseCaseOutput
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
