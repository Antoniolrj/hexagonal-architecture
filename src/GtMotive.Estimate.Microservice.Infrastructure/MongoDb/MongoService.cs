using System;
using GtMotive.Estimate.Microservice.Domain.Aggregates;
using GtMotive.Estimate.Microservice.Domain.Entities.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        private readonly string _databaseName;

        public MongoService(IOptions<MongoDbSettings> options)
        {
            MongoClient = new MongoClient(options.Value.ConnectionString);
            _databaseName = options.Value.MongoDbDatabaseName;
            RegisterBsonClasses();
        }

        public MongoClient MongoClient { get; }
        private IClientSessionHandle Session { get; set; }

        public static void RegisterBsonClasses()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Vehicle)))
            {
                BsonClassMap.RegisterClassMap<Vehicle>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(v => v.Id);
                    cm.MapProperty(v => v.LicensePlate).SetIsRequired(true);
                    cm.MapProperty(v => v.Model).SetIsRequired(true);
                    cm.MapProperty(v => v.ManufactureDate).SetIsRequired(true);
                    cm.MapProperty(v => v.IsAvailable).SetDefaultValue(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Reservation)))
            {
                BsonClassMap.RegisterClassMap<Reservation>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(r => r.Id);
                    cm.MapProperty(r => r.CustomerId).SetIsRequired(true);
                    cm.MapProperty(r => r.VehicleId).SetIsRequired(true);
                    cm.MapProperty(r => r.Status).SetIsRequired(true);
                    cm.MapProperty(r => r.ReservedAt).SetIsRequired(true);
                    cm.MapProperty(r => r.ReturnedAt).SetIgnoreIfNull(true); // Optional property
                });
            }
        }

        public IMongoDatabase GetDatabase()
        {
            if (string.IsNullOrEmpty(_databaseName))
            {
                throw new ArgumentException("Database name cannot be null or empty.", _databaseName);
            }

            return MongoClient.GetDatabase(_databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be null or empty.", nameof(collectionName));
            }

            return GetDatabase().GetCollection<T>(collectionName);
        }

        public IClientSessionHandle GetSession()
        {
            if (Session != null)
            {
                return Session;
            }

            Session = MongoClient.StartSession();
            return Session;
        }
    }
}
