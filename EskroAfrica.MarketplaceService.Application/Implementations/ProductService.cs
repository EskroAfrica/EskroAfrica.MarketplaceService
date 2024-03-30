using AutoMapper;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Domain.Entities;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        // get products for timeline
        // get user's products
        // get single product

        public ProductService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<PaginatedApiResponse<ProductResponse>> GetProductList(ProductRequestInput input)
        {
            // get products
            var products = await _unitOfWork.Repository<Product>().GetAllAsync(x =>
                input.CategoryId.HasValue ? x.CategoryId == input.CategoryId : true
                && input.SubCategoryId.HasValue ? x.SubCategoryId == input.SubCategoryId : true
                && input.SellerId.HasValue ? x.SellerId == input.SellerId : true
                && !string.IsNullOrEmpty(input.State) ? x.State.Contains(input.State, StringComparison.OrdinalIgnoreCase) : true
                && !string.IsNullOrEmpty(input.City) ? x.City.Contains(input.City, StringComparison.OrdinalIgnoreCase) : true
                && !string.IsNullOrEmpty(input.SearchTerm) ? x.Name.Contains(input.SearchTerm, StringComparison.OrdinalIgnoreCase) : true);

            // paginate
            var pageItems = MarketplaceServiceHelper.Paginate(products, input.PageNumber, input.PageSize);
            int total = products.Count();

            // map
            var mappedItems = _mapper.Map<ProductResponse>(pageItems);

            // return
            return PaginatedApiResponse<ProductResponse>.Response(mappedItems, "Successful", ApiResponseCode.Ok, total);
        }

        public async Task<ApiResponse<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _unitOfWork.Repository<Product>().GetAsync(x => x.Id == id);
            if (product == null) return ApiResponse<ProductResponse>.Response(null, "Product not found", ApiResponseCode.BadRequest);

            var response = _mapper.Map<ProductResponse>(product);
            return ApiResponse<ProductResponse>.Response(response, "Successful", ApiResponseCode.Ok);
        }
    }
}
