using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Domain.Entities;
using EskroAfrica.MarketplaceService.Infrastructure.Data;

namespace EskroAfrica.MarketplaceService.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarketplaceServiceDbContext _dbContext;
        private Dictionary<string, object> Repositories { get; set; } = new Dictionary<string, object>();

        public UnitOfWork(MarketplaceServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (Repositories.ContainsKey(nameof(T))) return (GenericRepository<T>)Repositories[nameof(T)];

            Repositories.Add(nameof(T), new GenericRepository<T>(_dbContext));
            return (GenericRepository<T>)Repositories[nameof(T)];
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
