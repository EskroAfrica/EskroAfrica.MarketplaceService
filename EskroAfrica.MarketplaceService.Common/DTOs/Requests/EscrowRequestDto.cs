namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class EscrowRequestDto
    {
        public string Name { get; set; }
        public Guid PayerIdentityUserId { get; set; }
        public Guid ReceiverIdentityUserId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
