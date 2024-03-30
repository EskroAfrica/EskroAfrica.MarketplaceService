using EskroAfrica.MarketplaceService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task SaveChangesAsync();
    }
}
