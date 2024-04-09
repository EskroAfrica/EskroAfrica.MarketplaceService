using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class SubCategoryRequest
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}
