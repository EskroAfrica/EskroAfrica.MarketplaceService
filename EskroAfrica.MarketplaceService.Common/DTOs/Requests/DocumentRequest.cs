using Microsoft.AspNetCore.Http;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class DocumentRequest
    {
        public List<IFormFile> Files { get; set; }
    }
}
