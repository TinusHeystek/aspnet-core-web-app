using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.Shared.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.App.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<TEntity> FindOrThrowAsync<TEntity>(this DbSet<TEntity> dbSet, int id, CancellationToken cancellationToken = new CancellationToken()) where TEntity : class
        {
            var entity = await dbSet.FindAsync(id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity)} with Id[{id}] not found.");
            return entity;
        }

        public static async Task<TModel> FindOrThrowAsync<TEntity, TModel>(this DbSet<TEntity> dbSet, int id, IMapper mapper, CancellationToken cancellationToken = new CancellationToken()) where TEntity : class, IId
        {
            var entity = await dbSet.Where(x => x.Id == id)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity)} with Id[{id}] not found.");
            return entity;
        }

        public static async Task<TEntity> FindOrThrowAsync<TEntity>(this IQueryable<TEntity> query, int id, CancellationToken cancellationToken = new CancellationToken()) where TEntity : class, IId
        {
            var entity = await query.Where(x => x.Id == id)
                .SingleOrDefaultAsync(cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity)} with Id[{id}] not found.");
            return entity;
        }

        public static async Task<TModel> FindOrThrowAsync<TEntity, TModel>(this IQueryable<TEntity> query, IMapper mapper, int id, CancellationToken cancellationToken = new CancellationToken()) where TEntity : class, IId
        {
            var entity = await query.Where(x => x.Id == id)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity)} with Id[{id}] not found.");
            return entity;
        }
    }
}
