using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponse>> GetProduct(Guid id);
        Task<PaginatedApiResponse<List<ProductResponse>>> GetProductList(ProductRequestInput input);
        Task<ApiResponse<ProductResponse>> AddProduct(ProductRequest request);
        Task<ApiResponse> MarkProductActiveOrInactive(Guid id);
        Task<ApiResponse<ProductResponse>> UpdateProduct(ProductUpdateRequest request);
        Task<ApiResponse> ApproveProduct(Guid id);
        Task<ApiResponse> RejectProduct(Guid id, string rejectionReason);
    }
}
