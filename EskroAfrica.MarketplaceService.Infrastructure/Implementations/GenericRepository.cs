using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Domain.Entities;
using EskroAfrica.MarketplaceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EskroAfrica.MarketplaceService.Infrastructure.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public DbSet<T> _entityDbSet { get; set; }

        public GenericRepository(MarketplaceServiceDbContext dbContext)
        {
            _entityDbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;

            _entityDbSet.Add(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entityDbSet.AsQueryable().Where(x => !x.IsDeleted);

            if(includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entityDbSet.AsQueryable().Where(x => !x.IsDeleted);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if(predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public void Update(T entity)
        {
            entity.UpdatedDate = DateTime.Now;

            _entityDbSet.Update(entity);
        }

        public void Delete(T entity, bool isSoftDelete = true)
        {
            if (isSoftDelete)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
            }
            else _entityDbSet.Remove(entity);
        }
    }
}
