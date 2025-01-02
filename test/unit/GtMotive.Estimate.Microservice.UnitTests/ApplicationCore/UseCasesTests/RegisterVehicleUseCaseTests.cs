using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Inputs;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Outputs;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.UseCasesTests
{
    public class RegisterVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<IRegisterVehicleOutputPort> _outputPortMock;
        private readonly RegisterVehicleUseCase _useCase;

        public RegisterVehicleUseCaseTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _outputPortMock = new Mock<IRegisterVehicleOutputPort>();
            _useCase = new RegisterVehicleUseCase(_vehicleRepositoryMock.Object);
            _useCase.SetOutputPort(_outputPortMock.Object);
        }

        [Fact]
        public async Task ExecuteShouldRegisterVehicleWhenInputIsValid()
        {
            var input = new RegisterVehicleInput
            {
                LicensePlate = "ABC123",
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureDate = DateTime.UtcNow
            };

            await _useCase.Execute(input);

            _vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>()), Times.Once);
            _outputPortMock.Verify(port => port.StandardHandle(It.IsAny<RegisterVehicleOutput>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteShouldThrowDomainExceptionWhenManufactureDateIsInvalid()
        {
            var input = new RegisterVehicleInput
            {
                LicensePlate = "ABC123",
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureDate = DateTime.UtcNow.AddYears(-5)
            };

            await Assert.ThrowsAsync<DomainException>(() => _useCase.Execute(input));
        }
    }
}
