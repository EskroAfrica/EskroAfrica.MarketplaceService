using EskroAfrica.MarketplaceService.Application.Implementations;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EskroAfrica.MarketplaceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : BaseController
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] SubCategoryRequest request)
            => CustomResponse(await _subCategoryService.AddSubCategory(request));

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubCategories([FromQuery] CategoryRequestInput input)
            => CustomResponse(await _subCategoryService.GetSubCategories(input));
    }
}
