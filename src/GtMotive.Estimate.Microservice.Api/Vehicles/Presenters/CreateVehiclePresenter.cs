using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Vehicles.Presenters
{
    public class CreateVehiclePresenter : IRegisterVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RegisterVehicleOutput response)
        {
            ActionResult = new ObjectResult(response);
        }
    }
}
