namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class SubCategory : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
