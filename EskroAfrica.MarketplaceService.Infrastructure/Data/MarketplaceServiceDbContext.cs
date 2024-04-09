using EskroAfrica.MarketplaceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EskroAfrica.MarketplaceService.Infrastructure.Data
{
    public class MarketplaceServiceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public MarketplaceServiceDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
