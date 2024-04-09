using AutoMapper;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Domain.Entities;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> AddSubCategory(SubCategoryRequest request)
        {
            var apiResponse = new ApiResponse();

            var sub = _mapper.Map<SubCategory>(request);
            _unitOfWork.Repository<SubCategory>().Add(sub);

            await _unitOfWork.SaveChangesAsync();
            return apiResponse.Success("Successful");
        }

        public async Task<ApiResponse<List<SubCategoryResponse>>> GetSubCategories(CategoryRequestInput input)
        {
            var apiResponse = new ApiResponse<List<SubCategoryResponse>>();

            var subs = (await _unitOfWork.Repository<SubCategory>().GetAllAsync(c =>
                !string.IsNullOrEmpty(input.SearchTerm) ? c.Name.Contains(input.SearchTerm) : true)).ToList();
            return apiResponse.Success(_mapper.Map<List<SubCategoryResponse>>(subs), "Successful", ApiResponseCode.Ok);
        }
    }
}
