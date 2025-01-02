using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Validations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Commands
{
    public class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        [Required(ErrorMessage = "Reservationid is required.")]
        [GuidValidation(ErrorMessage = "The value not is a GUID")]
        public string ReservationId { get; set; }
    }
}
