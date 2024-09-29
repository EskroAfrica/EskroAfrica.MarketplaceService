namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class ProductSaleRequest
    {
        public Guid ProductId { get; set; }
        public decimal SaleAmount { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndDate { get; set; }
    }
}
