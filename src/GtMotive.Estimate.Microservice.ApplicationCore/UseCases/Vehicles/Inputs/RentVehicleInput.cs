using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs
{
    public class RentVehicleInput
    {
        public Guid VehicleId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
