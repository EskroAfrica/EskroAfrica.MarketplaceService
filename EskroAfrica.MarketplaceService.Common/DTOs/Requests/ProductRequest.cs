using EskroAfrica.MarketplaceService.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool SellAsUnit { get; set; }
        public ProductCondition Condition { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string FeaturedImage { get; set; }
        public string AdditionalInformation { get; set; }
        public int CurrentQuantity { get; set; }

        public List<string> Images { get; set; }
    }
}
