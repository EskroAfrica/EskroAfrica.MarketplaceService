namespace EskroAfrica.MarketplaceService.Common.Models
{
    public class AppSettings
    {
        public IdentitySettings IdentitySettings { get; set; }
        public LogSettings LogSettings { get; set; }
    }

    public class IdentitySettings
    {
        public string Authority { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
    }

    public class LogSettings
    {
        public bool UseSeq { get; set; }
        public string LogUrl { get; set; }
    }
}
