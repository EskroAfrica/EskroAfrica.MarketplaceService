namespace EskroAfrica.MarketplaceService.Common.DTOs.Paystack
{
    public class PaystackResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class PaystackResponse<T> : PaystackResponse
    {
        public T data { get; set; }
    }
}
