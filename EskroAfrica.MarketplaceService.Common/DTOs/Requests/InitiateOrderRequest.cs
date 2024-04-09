namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class InitiateOrderRequest
    {
        public Guid ProductId { get; set; }
        public bool DeliveryRequired { get; set; }
        public Guid? DeliveryId { get; set; }
    }
}
