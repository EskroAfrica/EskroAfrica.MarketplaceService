using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface ISubCategoryService
    {
        Task<ApiResponse> AddSubCategory(SubCategoryRequest request);
        Task<ApiResponse<List<SubCategoryResponse>>> GetSubCategories(CategoryRequestInput input);
    }
}
