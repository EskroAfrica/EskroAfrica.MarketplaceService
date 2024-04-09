using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public Guid PayerId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
