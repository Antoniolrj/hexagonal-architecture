using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Vehicles.Presenters;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly RegisterVehicleUseCase _useCase;
        private readonly CreateVehiclePresenter _presenter;

        public CreateVehicleHandler(RegisterVehicleUseCase useCase, CreateVehiclePresenter presenter)
        {
            _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            var input = new RegisterVehicleInput
            {
                Model = request.Model,
                LicensePlate = request.LicensePlate,
                Brand = request.Brand,
                ManufactureDate = request.ManufacturingDate
            };

            _useCase.SetOutputPort(_presenter);
            await _useCase.Execute(input);

            return _presenter;
        }
    }
}
