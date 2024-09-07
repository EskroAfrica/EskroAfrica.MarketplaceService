using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponse>> GetProduct(Guid id);
        Task<PaginatedApiResponse<List<ProductResponse>>> GetProductList(ProductRequestInput input);
        Task<ApiResponse> AddProduct(ProductRequest request);
    }
}
