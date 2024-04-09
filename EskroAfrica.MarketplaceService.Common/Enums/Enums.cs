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
        QuoteGenerated,
        Pending,
        Processing,
        Completed,
        Canceled
    }

    public enum PickupMethod
    {
        SelfPickup,
        EskroDelivery
    }

    public enum OrderStatus
    {
        Initiated,
        Completed,
        Canceled
    }
}
