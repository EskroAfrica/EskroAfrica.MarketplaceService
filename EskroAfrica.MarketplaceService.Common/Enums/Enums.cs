using System.ComponentModel;

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
        [Description("Brand New")]
        BrandNew,
        [Description("Foreign Used")]
        ForeignUsed,
        [Description("Local Used")]
        LocalUsed
    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum ActiveStatus
    {
        Inactive,
        Active
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
        Canceled,
        Dispute,
        Closed
    }

    public enum NotificationType
    {
        Email,
        Push
    }
}
