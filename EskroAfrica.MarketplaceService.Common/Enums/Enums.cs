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
        ForeignUsed,
        LocalUsed
    }

    public enum ProductStatus
    {
        Pending,
        Approved,
        Rejected
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

    public enum NotificationType
    {
        Email,
        Push
    }
}
