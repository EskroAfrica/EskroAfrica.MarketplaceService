using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Paystack;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Common.Models;
using EskroAfrica.MarketplaceService.Domain.Entities;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPaystackService _paystackService;
        private readonly IKafkaProducerService _kafkaProducerService;

        public OrderService(IUnitOfWork unitOfWork, AppSettings appSettings, IJwtTokenService jwtTokenService, IPaystackService paystackService,
            IKafkaProducerService kafkaProducerService)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _jwtTokenService = jwtTokenService;
            _paystackService = paystackService;
            _kafkaProducerService = kafkaProducerService;
        }

        public async Task<ApiResponse<InitiateOrderResponse>> InitiateOrder(InitiateOrderRequest request)
        {
            var apiResponse = new ApiResponse<InitiateOrderResponse>();

            // get total amount (product, delivery)
            var product = await _unitOfWork.Repository<Product>().GetAsync(p => p.Id == request.ProductId);
            if (product == null) return apiResponse.Failure("Product not found", ApiResponseCode.BadRequest);
            if(product.Status != ProductStatus.Available) return apiResponse.Failure("Product is not available", ApiResponseCode.BadRequest);

            Delivery delivery = null;
            if (request.DeliveryRequired)
            {
                delivery = await _unitOfWork.Repository<Delivery>().GetAsync(d => d.Id == request.DeliveryId);
                if (delivery == null) return apiResponse.Failure("Delivery details not found", ApiResponseCode.BadRequest);
            }

            decimal totalPayable = product.Price + delivery.Amount;

            // create order
            var order = new Order
            {
                ProductId = product.Id,
                IdentityUserId = Guid.Parse(_jwtTokenService.IdentityUserId),
                PickupMethod = request.DeliveryRequired ? PickupMethod.EskroDelivery : PickupMethod.SelfPickup,
                Amount = totalPayable
            };
            _unitOfWork.Repository<Order>().Add(order);

            if(request.DeliveryRequired)
            {
                delivery.OrderId = order.Id;
            }
            _unitOfWork.Repository<Delivery>().Update(delivery);

            // initiate payment
            var initiateResponse = await _paystackService.InitiateTransaction
                (new InitiateTransactionRequest { Amount = (totalPayable * 100).ToString(), Email = _jwtTokenService.Email });
            if(initiateResponse == null || initiateResponse?.data == null || (!initiateResponse?.status ?? false))
                return apiResponse.Failure("Could not initiate payment", ApiResponseCode.BadRequest);

            // lock product and schedule unlock
            product.Status = ProductStatus.Locked;
            _unitOfWork.Repository<Product>().Update(product);

            await _unitOfWork.SaveChangesAsync();

            // schedule unlock

            // return payment link
            var response = new InitiateOrderResponse
            {
                OrderId = order.Id,
                AuthorizationUrl = initiateResponse.data.authauthorization_url,
            };

            return apiResponse.Success(response, "Successfully initiated order");
        }

        public async Task<ApiResponse> CompleteOrder(CompleteOrderRequest request)
        {
            var apiResponse = new ApiResponse();

            // get order, delivery, product
            var order = await _unitOfWork.Repository<Order>().GetAsync(o => o.Id == request.OrderId, o => o.Delivery);
            if (order == null) return apiResponse.Failure("Order not found");

            if(order.PickupMethod == PickupMethod.EskroDelivery && order.Delivery == null)
            {
                order.Delivery = await _unitOfWork.Repository<Delivery>().GetAsync(d => d.OrderId == order.Id);
            }

            var product = await _unitOfWork.Repository<Product>().GetAsync(p => p.Id == order.ProductId);
            if (product == null) return apiResponse.Failure("Product not found");

            // verify payment
            var verifyPaymentResponse = await _paystackService.VerifyTransaction(request.Reference);
            if (verifyPaymentResponse == null)
                return apiResponse.Failure("Could not verify payment", ApiResponseCode.BadRequest);
            if (!verifyPaymentResponse.status)
                return apiResponse.Failure("Payment was not successful", ApiResponseCode.BadRequest);
            if (verifyPaymentResponse.data.amount/100 < order.Amount)
                return apiResponse.Failure("Payment amount is less than actual order amount", ApiResponseCode.BadRequest);

            // update order details, update product details
            order.OrderStatus = OrderStatus.Completed;
            _unitOfWork.Repository<Order>().Update(order);

            product.Status = ProductStatus.Sold;
            _unitOfWork.Repository<Product>().Update(product);

            // save payment
            var payment = new Payment
            {
                Amount = order.Amount,
                OrderId = order.Id,
                PayerId = Guid.Parse(_jwtTokenService.IdentityUserId),
                ReceiverId = product.SellerId
            };
            _unitOfWork.Repository<Payment>().Add(payment);

            await _unitOfWork.SaveChangesAsync();

            // enqueue initiate delivery if delivery required
            if(order.PickupMethod == PickupMethod.EskroDelivery)
            {
                // enqueue
            }

            // send kafka message to escrow service
            var escrow = new EscrowRequestDto
            {
                Name = product.Name,
                PayerIdentityUserId = Guid.Parse(_jwtTokenService.IdentityUserId),
                ReceiverIdentityUserId = product.SellerId,
                ProductId = product.Id,
                PaymentId = payment.Id
            };
            await _kafkaProducerService.ProduceAsync(_appSettings.KafkaSettings.CreateEscrowTopic, escrow);

            // return
            return apiResponse.Success("Successfully completed order");
        }
    }
}
