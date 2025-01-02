using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Ports;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Presenters
{
    public class ReturnVehiclePresenter : IReturnVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle()
        {
            ActionResult = new StatusCodeResult(201);
        }
    }
}
