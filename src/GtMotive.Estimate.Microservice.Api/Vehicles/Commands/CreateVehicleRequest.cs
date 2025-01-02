using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string LicensePlate { get; set; }
    }
}
