using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Example.Core.Data
{
    public interface IDbContext : IDisposable
    {
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Guid> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitAsync(Guid transactiontId, CancellationToken cancellationToken = default);
        Task RollbackAsync(Guid transactionId, CancellationToken cancellationToken = default);
    }

    public class TransactionalDbContext : DbContext, IDbContext
    {
        // dotnet tool install --global dotnet-ef
        // dotnet add package Microsoft.EntityFrameworkCore.Design
        // dotnet ef migrations add InitialCreate
        // dotnet ef database update

        private IDbContextTransaction _transaction;
        private Guid _transactionId;

        public TransactionalDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (_transaction != null) 
                return await Task.FromResult(0);

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> BeginTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
        { 
            if (_transaction != null) 
                return Guid.Empty;

            _transaction = await Database.BeginTransactionAsync(cancellationToken);
            _transactionId = Guid.NewGuid();
            return _transactionId;
        }
 
        public async Task CommitAsync(Guid transactionId, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_transaction == null || transactionId != _transactionId) 
                return;

            try
            {
                await base.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
            }        
        }
 
        public async Task RollbackAsync(Guid transactionId, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_transaction == null || transactionId != _transactionId) 
                return;
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
    }
}