namespace EskroAfrica.MarketplaceService.Common.DTOs.Paystack
{
    public class VerifyTransactionResponse
    {
        public decimal amount { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
    }
}
