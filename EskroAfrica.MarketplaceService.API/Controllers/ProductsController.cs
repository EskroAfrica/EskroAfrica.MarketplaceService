using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetProductList([FromQuery] ProductRequestInput input)
            => CustomResponse(await _productService.GetProductList(input));

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
            => CustomResponse(await _productService.GetProduct(id));

        [HttpGet("/personal")]
        public async Task<IActionResult> GetUserProducts()
        {
            var response = await _productService.GetProductList(new ProductRequestInput { SellerId = Guid.Parse(_jwtTokenService.IdentityUserId) });
            return CustomResponse(response);
        }
    }
}
