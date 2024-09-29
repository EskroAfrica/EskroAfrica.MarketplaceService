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

        [HttpGet("seller-orders")]
        public async Task<IActionResult> GetSellerOrders(OrderRequestInput input)
            => CustomResponse(await _orderService.GetSellerOrders(input));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
            => CustomResponse(await (_orderService.GetOrder(id)));
    }
}
