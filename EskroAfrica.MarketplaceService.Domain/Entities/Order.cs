using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid IdentityUserId { get; set; }
        public PickupMethod PickupMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

        public Delivery Delivery { get; set; }
    }
}
