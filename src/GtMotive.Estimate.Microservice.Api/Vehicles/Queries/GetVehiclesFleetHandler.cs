using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Vehicles.Presenters;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Queries
{
    public class GetVehiclesFleetHandler : IRequestHandler<GetVehiclesFleetRequest, IWebApiPresenter>
    {
        private readonly GetVehiclesFleetUseCase _useCase;
        private readonly GetVehiclesFleetPresenter _presenter;

        public GetVehiclesFleetHandler(GetVehiclesFleetUseCase useCase, GetVehiclesFleetPresenter presenter)
        {
            _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task<IWebApiPresenter> Handle(GetVehiclesFleetRequest request, CancellationToken cancellationToken)
        {
            _useCase.SetOutputPort(_presenter);
            await _useCase.Execute(new GetVehiclesFleetInput());

            return _presenter;
        }
    }
}
