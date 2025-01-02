using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Vehicles.Presenters;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class RentVehicleHandler : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly RentVehicleUseCase _useCase;
        private readonly RentVehiclePresenter _presenter;

        public RentVehicleHandler(RentVehicleUseCase useCase, RentVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            var input = new RentVehicleInput
            {
                CustomerId = request.CustomerId,
                VehicleId = request.VehicleId
            };

            _useCase.SetOutputPort(_presenter);
            await _useCase.Execute(input);

            return _presenter;
        }
    }
}
