using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    public class RegisterVehicleUseCase : IUseCase<RegisterVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private IRegisterVehicleOutputPort _outputPort;

        public RegisterVehicleUseCase(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void SetOutputPort(IRegisterVehicleOutputPort outputPort)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public async Task Execute(RegisterVehicleInput input)
        {
            var vehicle = new Vehicle(new LicensePlate(input.LicensePlate), input.Brand, input.Model, ManufactureDate.Create(input.ManufactureDate));

            await _vehicleRepository.AddAsync(vehicle);

            var output = new RegisterVehicleOutput
            {
                VehicleId = vehicle.Id.Value,
            };

            _outputPort.StandardHandle(output);
        }
    }
}
