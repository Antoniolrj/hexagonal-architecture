using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MongoService _mongoService;
        private bool _disposed;

        public MongoUnitOfWork(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        public void BeginTransaction()
        {
            _mongoService.GetSession().StartTransaction();
        }

        public async Task<int> Save()
        {
            try
            {
                await _mongoService.GetSession().CommitTransactionAsync();
                return 1; // MongoDB do not have affected rows, to default, return 1.
            }
            catch (Exception)
            {
                await _mongoService.GetSession().AbortTransactionAsync();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _mongoService.GetSession().Dispose();
            }

            _disposed = true;
        }
    }
}
