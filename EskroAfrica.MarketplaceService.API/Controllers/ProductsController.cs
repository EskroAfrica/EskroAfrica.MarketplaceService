using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EskroAfrica.MarketplaceService.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogService _logService;

        public ProductsController(IProductService productService, IJwtTokenService jwtTokenService, ILogService logService)
        {
            _productService = productService;
            _jwtTokenService = jwtTokenService;
            _logService = logService;
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

        [HttpGet("/test")]
        [AllowAnonymous]
        public async Task<IActionResult> test()
        {
            var obj = new { name = "emeka", age = 12 };
            int count = 2;

            _logService.LogEvent(Serilog.Events.LogEventLevel.Information, "logging {@obj} for the {count} time", obj, count);
            return Ok(obj);
        }
    }
}
