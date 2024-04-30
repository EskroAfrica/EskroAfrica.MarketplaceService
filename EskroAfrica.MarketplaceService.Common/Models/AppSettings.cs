using Confluent.Kafka;

namespace EskroAfrica.MarketplaceService.Common.Models
{
    public class AppSettings
    {
        public IdentitySettings IdentitySettings { get; set; }
        public LogSettings LogSettings { get; set; }
        public PaystackSettings PaystackSettings { get; set; }
        public EscrowSettings EscrowSettings { get; set; }
        public KafkaSettings KafkaSettings { get; set; }
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
        public string LogRef { get; set; } = Guid.NewGuid().ToString();
    }

    public class PaystackSettings
    {
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
    }

    public class EscrowSettings
    {
        public decimal ProcessingFeePercentage { get; set; }
    }

    public class KafkaSettings
    {
        public ProducerConfig ProducerConfig { get; set; }
        public ConsumerConfig ConsumerConfig { get; set; }
        public string CreateEscrowTopic { get; set; }
    }
}
