using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.Vehicles.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    [Collection(TestCollections.Functional)]
    public class CreateVehicleTests : FunctionalTestBase
    {
        public CreateVehicleTests(CompositionRootTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task CreateVehicle_IntegrationTest_Success()
        {
            var vehicleId = Guid.Empty;

            await Fixture.UsingHandlerForRequestResponse<CreateVehicleRequest, IWebApiPresenter>(async handler =>
            {
                var request = new CreateVehicleRequest
                {
                    Model = "Test model",
                    LicensePlate = "1234ABC",
                    Brand = "Test brand",
                    ManufacturingDate = DateTime.UtcNow
                };

                var presenter = await handler.Handle(request, default);
                var objectResult = presenter.ActionResult as ObjectResult;
                var value = objectResult?.Value as RegisterVehicleOutput;

                vehicleId = value.VehicleId;

                Assert.NotNull(presenter);
                Assert.IsType<ObjectResult>(presenter.ActionResult);
            });

            await Fixture.UsingRepository<IVehicleRepository>(async repository =>
            {
                var vehicle = await repository.GetByIdAsync(new VehicleId(vehicleId));
                Assert.NotNull(vehicle);
            });
        }
    }
}
