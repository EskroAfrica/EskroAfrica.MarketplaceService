using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public Guid OrderId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

        public Order Order { get; set; }
    }
}
