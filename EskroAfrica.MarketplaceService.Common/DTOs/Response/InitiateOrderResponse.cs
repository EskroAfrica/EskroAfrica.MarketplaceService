namespace EskroAfrica.MarketplaceService.Common.DTOs.Response
{
    public class InitiateOrderResponse
    {
        public Guid OrderId { get; set; }
        public string AuthorizationUrl { get; set; }
    }
}
