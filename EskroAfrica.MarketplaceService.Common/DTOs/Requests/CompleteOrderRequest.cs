namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class CompleteOrderRequest
    {
        public Guid OrderId { get; set; }
        public string Reference { get; set; }
    }
}
