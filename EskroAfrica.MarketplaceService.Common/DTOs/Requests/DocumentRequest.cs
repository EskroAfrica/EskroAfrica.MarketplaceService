using Microsoft.AspNetCore.Http;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class DocumentRequest
    {
        public List<string>? Base64Strings { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
