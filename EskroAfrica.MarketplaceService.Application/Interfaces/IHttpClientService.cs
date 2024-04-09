namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> headers);
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string url, Dictionary<string, string> headers);
    }
}
