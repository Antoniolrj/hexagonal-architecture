using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Vehicles.Presenters;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class ReturnVehicleHandler : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        private readonly ReturnVehicleUseCase _useCase;
        private readonly ReturnVehiclePresenter _presenter;

        public ReturnVehicleHandler(ReturnVehicleUseCase useCase, ReturnVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            var input = new ReturnVehicleInput
            {
                ReservationId = request.ReservationId
            };

            _useCase.SetOutputPort(_presenter);
            await _useCase.Execute(input);

            return _presenter;
        }
    }
}
