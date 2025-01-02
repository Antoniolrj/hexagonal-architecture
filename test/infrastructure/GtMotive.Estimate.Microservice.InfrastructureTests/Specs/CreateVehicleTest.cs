using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Vehicles.Commands;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Newtonsoft.Json;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    public class CreateVehicleTest : InfrastructureTestBase
    {
        private readonly HttpClient _client;
        private readonly string _endpointUrl = "/api/vehicles";

        public CreateVehicleTest(GenericInfrastructureTestServerFixture fixture)
            : base(fixture)
        {
            _client = fixture.Server.CreateClient();
        }

        [Fact]
        public async Task CreateVehicle_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                LicensePlate = "1234ABC",
                ManufacturingDate = DateTime.UtcNow
            };

            using var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var uri = new Uri(_endpointUrl, UriKind.Relative);

            var response = await _client.PostAsync(uri, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_ReturnsVehicleId_WhenModelIsValid()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                Model = "Model",
                Brand = "Brand",
                LicensePlate = "1234ABC",
                ManufacturingDate = DateTime.UtcNow
            };

            using var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var uri = new Uri(_endpointUrl, UriKind.Relative);

            var response = await _client.PostAsync(uri, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
