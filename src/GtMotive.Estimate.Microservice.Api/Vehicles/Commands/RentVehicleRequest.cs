using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Validations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        [Required(ErrorMessage = "VehicleId is required.")]
        [GuidValidation(ErrorMessage = "The value not is a GUID")]
        public string VehicleId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        [GuidValidation(ErrorMessage = "The value not is a GUID")]
        public string CustomerId { get; set; }
    }
}
