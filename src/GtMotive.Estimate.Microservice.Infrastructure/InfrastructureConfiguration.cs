using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped(typeof(ITelemetry), typeof(AppTelemetry));
            }
            else
            {
                services.AddScoped(typeof(ITelemetry), typeof(NoOpTelemetry));
            }

            var mongoSetting = configuration.GetSection("MongoDb");
            services.Configure<MongoDbSettings>(options => mongoSetting.Bind(options));

            services.AddSingleton<MongoService>();
            services.AddSingleton(provider =>
            {
                var mongoService = provider.GetRequiredService<MongoService>();
                var settings = provider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return mongoService.GetDatabase();
            });

            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            return new InfrastructureBuilder(services);
        }

        private sealed class InfrastructureBuilder : IInfrastructureBuilder
        {
            public InfrastructureBuilder(IServiceCollection services)
            {
                Services = services;
            }

            public IServiceCollection Services { get; }
        }
    }
}
