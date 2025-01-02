using System;
using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        public Guid ReservationId { get; set; }
    }
}
