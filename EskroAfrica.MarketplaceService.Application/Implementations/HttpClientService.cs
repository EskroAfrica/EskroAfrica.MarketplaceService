using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.Models;
using Newtonsoft.Json;
using Serilog.Events;
using System.Net.Http.Json;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogService _logService;
        private readonly AppSettings _appSettings;

        public HttpClientService(IHttpClientFactory httpClientFactory, ILogService logService, AppSettings appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _logService = logService;
            _appSettings = appSettings;
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers)
        {
            var client = GetClient(headers);
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            _logService.LogEvent(LogEventLevel.Information,
                _appSettings.LogSettings.LogRef + ": HTTP GET Request: {@responseString}", responseString);

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string url, Dictionary<string, string> headers)
        {
            var client = GetClient(headers);
            var response = await client.PostAsJsonAsync(url, request);

            var responseString = await response.Content.ReadAsStringAsync();

            _logService.LogEvent(LogEventLevel.Information,
                _appSettings.LogSettings.LogRef + ": HTTP POST Request: {@request} {@responseString}", request, responseString);

            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }

        private HttpClient GetClient(Dictionary<string, string> headers)
        {
            var client = _httpClientFactory.CreateClient();
            if (headers != null)
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return client;
        }
    }
}
