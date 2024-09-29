using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? AdditionalInformation { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
        public ProductCondition Condition { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string FeaturedImage { get; set; }
        public string Images { get; set; }
        public int Quantity { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
        public bool IsOnSale { get; set; }
        public decimal SaleAmount { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndDate { get; set; }
        public string RejectionReason { get; set; }

        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
