namespace EskroAfrica.MarketplaceService.Common.Enums
{
    public enum ApiResponseCode
    {
        Ok,
        ProcessingError,
        BadRequest,
        Forbidden
    }

    public enum ProductCondition
    {
        BrandNew,
        Used
    }

    public enum ProductStatus
    {
        Available,
        Locked,
        Sold,
        Unavailable
    }

    public enum DeliveryStatus
    {
        Pending,
        Processing,
        Completed
    }
}
