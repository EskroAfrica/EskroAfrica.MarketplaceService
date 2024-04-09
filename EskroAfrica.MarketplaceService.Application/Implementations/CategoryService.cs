using AutoMapper;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Domain.Entities;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> AddCategory(CategoryRequest request)
        {
            var apiResponse = new ApiResponse();

            var category = _mapper.Map<Category>(request);
            _unitOfWork.Repository<Category>().Add(category);

            await _unitOfWork.SaveChangesAsync();
            return apiResponse.Success("Successful");
        }

        public async Task<ApiResponse<List<CategoryResponse>>> GetCategories(CategoryRequestInput input)
        {
            var apiResponse = new ApiResponse<List<CategoryResponse>>();

            var categories = (await _unitOfWork.Repository<Category>().GetAllAsync(c => 
                !string.IsNullOrEmpty(input.SearchTerm) ? c.Name.Contains(input.SearchTerm) : true, w => w.SubCategories)).ToList();
            return apiResponse.Success(_mapper.Map<List<CategoryResponse>>(categories), "Successful", ApiResponseCode.Ok);
        }
    }
}
