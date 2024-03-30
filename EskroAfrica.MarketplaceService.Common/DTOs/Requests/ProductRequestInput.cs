using System.Text.Json.Serialization;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class ProductRequestInput
    {
        public string SearchTerm { get; set; }
        [JsonIgnore]
        public Guid? SellerId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
