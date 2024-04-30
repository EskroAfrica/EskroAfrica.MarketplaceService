using Confluent.Kafka;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.Models;
using Newtonsoft.Json;
using Serilog.Events;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogService _logService;

        public KafkaProducerService(AppSettings appSettings, ILogService logService)
        {
            _appSettings = appSettings;
            _logService = logService;
        }

        public async Task ProduceAsync<T>(string topic, T message)
        {
            using (var producer = new ProducerBuilder<string, string>(_appSettings.KafkaSettings.ProducerConfig).Build())
            {
                try
                {
                    _logService.LogEvent(LogEventLevel.Information, "Producing to {@topic} - Message: {@message}", topic, message);
                    await producer.ProduceAsync(topic, new Message<string, string>
                    {
                        Key = Guid.NewGuid().ToString(),
                        Value = JsonConvert.SerializeObject(message)
                    });
                    _logService.LogEvent(LogEventLevel.Information, "Produced to {@topic} - Message: {@message}", topic, message);
                }
                catch (Exception ex)
                {
                    _logService.LogEvent(LogEventLevel.Error, "Could not produce to {@topic} - Exception: {@ex}", topic, ex);
                }
            }
        }
    }
}
