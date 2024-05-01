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
        // add product
        // remove product from market

        public ProductService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<PaginatedApiResponse<List<ProductResponse>>> GetProductList(ProductRequestInput input)
        {
            var apiResponse = new PaginatedApiResponse<List<ProductResponse>>();

            // get products
            var products = await _unitOfWork.Repository<Product>().GetAllAsync(x =>
                input.CategoryId.HasValue ? x.CategoryId == input.CategoryId : true
                && input.SubCategoryId.HasValue ? x.SubCategoryId == input.SubCategoryId : true
                && input.SellerId.HasValue ? x.SellerId == input.SellerId : true
                && !string.IsNullOrEmpty(input.State) ? x.State.Contains(input.State) : true
                && !string.IsNullOrEmpty(input.City) ? x.City.Contains(input.City) : true
                && !string.IsNullOrEmpty(input.SearchTerm) ? x.Name.Contains(input.SearchTerm) : true);

            // paginate
            var pageItems = MarketplaceServiceHelper.Paginate(products, input.PageNumber, input.PageSize);
            int total = products.Count();

            // map
            var mappedItems = _mapper.Map<List<ProductResponse>>(pageItems);

            // return
            return apiResponse.Success(mappedItems, "Successful", ApiResponseCode.Ok, total);
        }

        public async Task<ApiResponse<ProductResponse>> GetProduct(Guid id)
        {
            var apiResponse = new ApiResponse<ProductResponse>();

            var product = await _unitOfWork.Repository<Product>().GetAsync(x => x.Id == id);
            if (product == null) return apiResponse.Failure("Product not found");

            var response = _mapper.Map<ProductResponse>(product);
            return apiResponse.Success(response, "Successful");
        }

        public async Task<ApiResponse> AddProduct(ProductRequest request)
        {
            var apiResponse = new ApiResponse();

            var product = _mapper.Map<Product>(request);
            product.SellerId = Guid.Parse(_jwtTokenService.IdentityUserId);
            product.Status = ProductStatus.Available;

            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.SaveChangesAsync();

            // notify customer

            return apiResponse.Success("Successful");
        }
    }
}
