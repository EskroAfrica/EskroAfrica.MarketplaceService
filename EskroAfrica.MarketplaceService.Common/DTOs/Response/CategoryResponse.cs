using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Response
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategoryResponse> SubCategories { get; set; }
    }
}
