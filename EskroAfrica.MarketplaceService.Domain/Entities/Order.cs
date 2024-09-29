using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public PickupMethod PickupMethod { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal ProcessingFeePercentage { get; set; }
        public bool PayerConsentedPayment { get; set; }
        public DateTime? PayerConsentedPaymentDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? DisputedDate { get; set; }
        public DateTime? CanceledDate { get; set; }

        public Delivery Delivery { get; set; }
        public Product Product { get; set; }
    }
}
