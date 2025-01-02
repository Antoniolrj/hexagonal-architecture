using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Presenters
{
    public class GetVehiclesFleetPresenter : IGetVehiclesFleetOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(GetVehiclesFleetOutput response)
        {
            ActionResult = new ObjectResult(response);
        }
    }
}
