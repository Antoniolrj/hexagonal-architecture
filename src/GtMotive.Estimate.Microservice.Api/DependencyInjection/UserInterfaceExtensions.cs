using GtMotive.Estimate.Microservice.Api.Vehicles.Presenters;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<IRegisterVehicleOutputPort, CreateVehiclePresenter>();
            services.AddScoped<GetVehiclesFleetPresenter>();
            services.AddScoped<IGetVehiclesFleetOutputPort, GetVehiclesFleetPresenter>();
            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<IRentVehicleOutputPort, RentVehiclePresenter>();
            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<IReturnVehicleOutputPort, ReturnVehiclePresenter>();

            return services;
        }
    }
}
