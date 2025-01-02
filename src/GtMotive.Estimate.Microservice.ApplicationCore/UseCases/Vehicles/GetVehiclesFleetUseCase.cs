using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    public class GetVehiclesFleetUseCase : IUseCase<GetVehiclesFleetInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private IGetVehiclesFleetOutputPort _outputPort;

        public GetVehiclesFleetUseCase(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void SetOutputPort(IGetVehiclesFleetOutputPort outputPort)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public async Task Execute(GetVehiclesFleetInput input)
        {
            var vehicles = await _vehicleRepository.GetAvailableVehiclesAsync();

            var output = new GetVehiclesFleetOutput
            {
                Vehicles = vehicles
            };

            _outputPort.StandardHandle(output);
        }
    }
}
