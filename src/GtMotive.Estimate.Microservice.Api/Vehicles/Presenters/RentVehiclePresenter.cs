using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Ports;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Presenters
{
    public class RentVehiclePresenter : IRentVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RentVehicleOutput response)
        {
            ActionResult = new ObjectResult(response);
        }
    }
}
