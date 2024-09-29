using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Paystack;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Common.Models;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class PaystackService : IPaystackService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly AppSettings _appSettings;
        private readonly Dictionary<string, string> _paystackHeader;
        private readonly string _baseUrl;

        public PaystackService(IHttpClientService httpClientService, AppSettings appSettings)
        {
            _httpClientService = httpClientService;
            _appSettings = appSettings;
            _paystackHeader = new Dictionary<string, string>()
            {
                {"Authorization", $"Bearer {appSettings.PaystackSettings.SecretKey}"},
                {"Content-Type", "application/json"}
            };
            _baseUrl = _appSettings.PaystackSettings.BaseUrl;
        }

        public async Task<PaystackResponse<InitiateTransactionResponse>> InitiateTransaction(InitiateTransactionRequest request)
        {
            var response = await _httpClientService.PostAsync<InitiateTransactionRequest, PaystackResponse<InitiateTransactionResponse>>
                (request, $"{_baseUrl}/transaction/initialize", _paystackHeader);

            return response;
        }

        public async Task<PaystackResponse<VerifyTransactionResponse>> VerifyTransaction(string reference)
        {
            var response = await _httpClientService.GetAsync<PaystackResponse<VerifyTransactionResponse>>
                ($"{_baseUrl}/transaction/verify/{reference}", _paystackHeader);

            return response;
        }

        public async Task CreateWallet(CreateWalletRequest request)
        {
            var response = await _httpClientService.PostAsync<CreateWalletRequest, PaystackResponse>
                (request, $"{_baseUrl}/dedicated_account/assign", _paystackHeader);
        }
    }
}
