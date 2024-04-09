using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Response
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public string UserMessage { get; set; }
        public ApiResponseCode ResponseCode { get; set; }

        public ApiResponse Success(string message, ApiResponseCode responseCode = ApiResponseCode.Ok, string userMessage = null)
        {
            Message = message;
            UserMessage = userMessage ?? message;
            ResponseCode = responseCode;

            return this;
        }

        public ApiResponse Failure(string message, ApiResponseCode responseCode = ApiResponseCode.BadRequest, string userMessage = null)
        {
            Message = message;
            UserMessage = userMessage ?? message;
            ResponseCode = responseCode;

            return this;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse<T> Success(T data, string message, ApiResponseCode responseCode = ApiResponseCode.Ok, string userMessage = null)
        {
            Message = message;
            UserMessage = userMessage ?? message;
            ResponseCode = responseCode;
            Data = data;

            return this;
        }

        public ApiResponse<T> Failure(string message, ApiResponseCode responseCode = ApiResponseCode.BadRequest, string userMessage = null)
        {
            Message = message;
            UserMessage = userMessage ?? message;
            ResponseCode = responseCode;

            return this;
        }
    }

    public class PaginatedApiResponse<T> : ApiResponse<T>
    {
        public int TotalCount { get; set; }

        public PaginatedApiResponse<T> Success(T data, string message, ApiResponseCode responseCode = ApiResponseCode.Ok, int total = 0, string userMessage = null)
        {
            Message = message;
            UserMessage = userMessage ?? message;
            ResponseCode = responseCode;
            Data = data;
            TotalCount = total;

            return this;
        }

        public PaginatedApiResponse<T> Failure(string message, ApiResponseCode responseCode = ApiResponseCode.BadRequest, string userMessage = null)
        {
            Message = message;
            UserMessage = userMessage ?? message;
            ResponseCode = responseCode;

            return this;
        }
    }
}
