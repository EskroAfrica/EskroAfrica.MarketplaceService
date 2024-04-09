using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EskroAfrica.MarketplaceService.API.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("initiate")]
        public async Task<IActionResult> InitiateOrder([FromBody] InitiateOrderRequest request)
            => CustomResponse(await _orderService.InitiateOrder(request));

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteOrder([FromBody] CompleteOrderRequest request)
            => CustomResponse(await _orderService.CompleteOrder(request));
    }
}
