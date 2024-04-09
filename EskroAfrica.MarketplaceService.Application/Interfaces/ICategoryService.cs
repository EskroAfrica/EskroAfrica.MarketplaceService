using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse> AddCategory(CategoryRequest request);
        Task<ApiResponse<List<CategoryResponse>>> GetCategories(CategoryRequestInput input);
    }
}
