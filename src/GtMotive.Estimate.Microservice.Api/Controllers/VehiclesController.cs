using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Vehicles.Commands;
using GtMotive.Estimate.Microservice.Api.Vehicles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody][Required] CreateVehicleRequest request)
        {
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpGet("availables")]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            var presenter = await _mediator.Send(new GetVehiclesFleetRequest());
            return presenter.ActionResult;
        }

        [HttpPost("rent")]
        public async Task<IActionResult> RentVehicle([FromBody][Required] RentVehicleRequest request)
        {
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnVehicle([FromBody][Required] ReturnVehicleRequest request)
        {
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }
    }
}
