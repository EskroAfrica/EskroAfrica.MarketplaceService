using System.Linq.Expressions;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        void Update(T entity);
        void Delete(T entity, bool isSoftDelete = true);
    }
}
