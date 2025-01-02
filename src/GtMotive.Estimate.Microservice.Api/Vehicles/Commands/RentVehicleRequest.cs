using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        [Required(ErrorMessage = "VehicleId is required.")]
        public Guid VehicleId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public Guid CustomerId { get; set; }
    }
}
