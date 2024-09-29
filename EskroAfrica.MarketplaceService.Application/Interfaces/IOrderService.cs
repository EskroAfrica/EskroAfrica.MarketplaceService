using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<InitiateOrderResponse>> InitiateOrder(InitiateOrderRequest request);
        Task<ApiResponse> CompleteOrder(CompleteOrderRequest request);
        Task<PaginatedApiResponse<List<OrderResponse>>> GetSellerOrders(OrderRequestInput input);
        Task<ApiResponse<OrderResponse>> GetOrder(Guid orderId);
    }
}
