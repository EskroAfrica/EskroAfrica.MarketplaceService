using AutoMapper;
using Azure.Core;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Common.Models;
using EskroAfrica.MarketplaceService.Domain.Entities;
using Hangfire;
using Newtonsoft.Json;
using System.Text.Encodings.Web;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IKafkaProducerService _kafkaProducerService;
        private readonly IGenericRepository<Product> _productRepository;

        // get products for timeline
        // get user's products
        // get single product
        // add product
        // remove product from market

        public ProductService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IMapper mapper,
            AppSettings appSettings, IKafkaProducerService kafkaProducerService)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.Repository<Product>();
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _appSettings = appSettings;
            _kafkaProducerService = kafkaProducerService;
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
                && !string.IsNullOrEmpty(input.SearchTerm) ? x.Name.Contains(input.SearchTerm) : true
                && x.ApprovalStatus == ApprovalStatus.Approved
                && x.ActiveStatus == ActiveStatus.Active);

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

        public async Task<ApiResponse<ProductResponse>> AddProduct(ProductRequest request)
        {
            var apiResponse = new ApiResponse<ProductResponse>();

            var product = _mapper.Map<Product>(request);
            product.SellerId = Guid.Parse(_jwtTokenService.IdentityUserId);
            product.ApprovalStatus = ApprovalStatus.Pending;

            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.SaveChangesAsync();
            
            BackgroundJob.Enqueue(() => PostAddProductAction(product.Id, _jwtTokenService.Email));

            return apiResponse.Success(_mapper.Map<ProductResponse>(product), "Successful");
        }

        public async Task<ApiResponse<ProductResponse>> UpdateProduct(ProductUpdateRequest request)
        {
            var apiResponse = new ApiResponse<ProductResponse>();

            var product = await _productRepository.GetAsync(x => x.Id == request.ProductId);
            if (product == null) return apiResponse.Failure("Product not found");
            if (product.SellerId != Guid.Parse(_jwtTokenService.IdentityUserId)) return apiResponse.Failure("You are not the owner of this product");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.Condition = request.Condition;
            product.State = request.State;
            product.City = request.City;
            product.AdditionalInformation = request.AdditionalInformation;
            product.Address = request.Address;
            product.CategoryId = request.CategoryId;
            product.SubCategoryId = request.SubCategoryId;
            product.FeaturedImage = request.FeaturedImage;
            product.Images = JsonConvert.SerializeObject(request.Images);
            product.ApprovalStatus = ApprovalStatus.Pending;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return apiResponse.Success(_mapper.Map<ProductResponse>(product), "Successful");
        }

        public async Task PostAddProductAction(Guid productId, string sellerEmail)
        {
            var product = await _productRepository.GetAsync(p => p.Id == productId);
            if (product == null) return;

            var notifications = new List<NotificationRequest>();
            // notify user
            var userEmailNotification = MarketplaceServiceHelper.CreateNotificationRequest
                ("New Product Added", _appSettings.NotificationSettings.SellerAddProductEmailMessage, 
                NotificationType.Email, new List<string> { sellerEmail });

            // notify admin of new pending product
            var adminEmailNotification = MarketplaceServiceHelper.CreateNotificationRequest
                ("New Product Added", _appSettings.NotificationSettings.AdminAddProductEmailMessage, 
                NotificationType.Email, _appSettings.NotificationSettings.AdminEmails);

            notifications.Add(userEmailNotification);
            notifications.Add(adminEmailNotification);

            await _kafkaProducerService.ProduceAsync(_appSettings.KafkaSettings.Topics.NotificationTopic, notifications);
        }

        public async Task<ApiResponse> MarkProductActiveOrInactive(Guid id)
        {
            var apiResponse = new ApiResponse<ProductResponse>();

            var product = await _productRepository.GetAsync(x => x.Id == id);
            if (product == null) return apiResponse.Failure("Product not found");
            if(product.SellerId != Guid.Parse(_jwtTokenService.IdentityUserId)) return apiResponse.Failure("You are not the owner of this product");

            product.ActiveStatus = product.ActiveStatus == ActiveStatus.Active ? ActiveStatus.Inactive : ActiveStatus.Active;
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return apiResponse.Success("Success");
        }

        public async Task<ApiResponse> SetSale(ProductSaleRequest request)
        {
            var apiResponse = new ApiResponse<ProductResponse>();

            var product = await _productRepository.GetAsync(x => x.Id == request.ProductId);
            if (product == null) return apiResponse.Failure("Product not found");
            if (product.SellerId != Guid.Parse(_jwtTokenService.IdentityUserId)) return apiResponse.Failure("You are not the owner of this product");

            product.IsOnSale = true;
            product.SaleStartDate = request.SaleStartDate;
            product.SaleEndDate = request.SaleEndDate;

            BackgroundJob.Schedule(() => EndSale(product.Id), product.SaleEndDate);

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return apiResponse.Success("Success");
        }

        public async Task EndSale(Guid id)
        {
            var product = await _productRepository.GetAsync(x => x.Id == id);
            if (product == null) return;

            product.IsOnSale = false;
            product.SaleStartDate = default;
            product.SaleEndDate = default;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
