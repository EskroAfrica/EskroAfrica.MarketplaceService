using EskroAfrica.MarketplaceService.Common.Enums;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Requests
{
    public class NotificationRequest
    {
        public NotificationType Type { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
    }
}
