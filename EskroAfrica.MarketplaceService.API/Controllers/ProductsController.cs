using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EskroAfrica.MarketplaceService.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IJwtTokenService _jwtTokenService;

        public ProductsController(IProductService productService, IJwtTokenService jwtTokenService)
        {
            _productService = productService;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetProductList([FromQuery] ProductRequestInput input)
            => CustomResponse(await _productService.GetProductList(input));

        [HttpGet]
        [Route(("{id}"))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetProduct(Guid id)
            => CustomResponse(await _productService.GetProduct(id));

        [HttpGet]
        [Route("personal")]
        [ProducesResponseType(typeof(PaginatedApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetUserProducts()
            => CustomResponse(await _productService.GetProductList(new ProductRequestInput { SellerId = Guid.Parse(_jwtTokenService.IdentityUserId) }));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> AddProduct(ProductRequest request)
            => CustomResponse(await _productService.AddProduct(request));

        [HttpPost("mark-active-or-inactive/{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> MarkProductActiveOrInactive(Guid id)
            => CustomResponse(await _productService.MarkProductActiveOrInactive(id));

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateProduct(ProductUpdateRequest request)
            => CustomResponse(await _productService.UpdateProduct(request));

        [HttpPost("approve/{id}")]
        [Authorize(Policy = "CanAccessBackOffice")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> ApproveProduct(Guid id)
            => CustomResponse(await _productService.ApproveProduct(id));

        [HttpPost("reject/{id}")]
        [Authorize(Policy = "CanAccessBackOffice")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> RejectProduct(Guid id, [FromQuery] string rejectionReason)
            => CustomResponse(await _productService.RejectProduct(id, rejectionReason));
    }
}
