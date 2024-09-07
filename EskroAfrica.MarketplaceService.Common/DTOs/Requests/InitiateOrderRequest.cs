namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class InitiateOrderRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool DeliveryRequired { get; set; }
        public DeliveryRequest DeliveryRequest { get; set; }
    }
}
