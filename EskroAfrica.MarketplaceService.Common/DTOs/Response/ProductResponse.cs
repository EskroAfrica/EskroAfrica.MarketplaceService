using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public decimal Price { get; set; }
        public ProductCondition Condition { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public ApprovalStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string FeaturedImage { get; set; }
        public int Quantity { get; set; }
        public List<string> Images { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
        public bool IsOnSale { get; set; }
        public decimal SaleAmount { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndDate { get; set; }
    }
}
