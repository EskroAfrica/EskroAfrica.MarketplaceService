namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class DeliveryRequest
    {
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
    }
}
