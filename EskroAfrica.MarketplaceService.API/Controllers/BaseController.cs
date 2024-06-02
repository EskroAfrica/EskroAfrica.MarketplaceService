using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EskroAfrica.MarketplaceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "CanAccessApp")]
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(ApiResponse response)
        {
            switch (response.ResponseCode)
            {
                case ApiResponseCode.Ok: return Ok(response);
                case ApiResponseCode.BadRequest:
                case ApiResponseCode.ProcessingError:
                case ApiResponseCode.Forbidden:
                    return BadRequest(response);
                default: return BadRequest(response);
            }
        }
    }
}
