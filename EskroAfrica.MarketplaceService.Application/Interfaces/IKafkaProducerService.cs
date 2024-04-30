namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync<T>(string topic, T message);
    }
}
