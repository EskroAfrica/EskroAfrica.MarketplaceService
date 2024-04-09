using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
        public ProductCondition Condition { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public ProductStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string FeaturedImage { get; set; }
        public string Images { get; set; }

        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
