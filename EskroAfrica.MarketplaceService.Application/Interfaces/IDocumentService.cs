using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<ApiResponse<List<string>>> Upload(DocumentRequest request);
    }
}
