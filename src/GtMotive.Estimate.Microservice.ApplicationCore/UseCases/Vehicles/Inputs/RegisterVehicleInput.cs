using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs
{
    public class RegisterVehicleInput : IUseCaseInput
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public DateTime ManufactureDate { get; set; }
    }
}
