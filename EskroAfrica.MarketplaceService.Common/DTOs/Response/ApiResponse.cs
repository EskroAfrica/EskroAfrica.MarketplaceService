using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Response
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public string UserMessage { get; set; }
        public ApiResponseCode ResponseCode { get; set; }

        public static ApiResponse Response(string message, ApiResponseCode responseCode, string userMessage = null)
        {
            return new ApiResponse
            {
                Message = message,
                UserMessage = userMessage ?? message,
                ResponseCode = responseCode
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public static ApiResponse<T> Response(T data, string message, ApiResponseCode responseCode, string userMessage = null)
        {
            return new ApiResponse<T>
            {
                Message = message,
                UserMessage = userMessage ?? message,
                ResponseCode = responseCode,
                Data = data
            };
        }
    }

    public class PaginatedApiResponse<T> : ApiResponse<T>
    {
        public int TotalCount { get; set; }

        public static PaginatedApiResponse<T> Response(T data, string message, ApiResponseCode responseCode, int total = 0, string userMessage = null)
        {
            return new PaginatedApiResponse<T>
            {
                Message = message,
                UserMessage = userMessage ?? message,
                ResponseCode = responseCode,
                Data = data,
                TotalCount = total
            };
        }
    }
}
