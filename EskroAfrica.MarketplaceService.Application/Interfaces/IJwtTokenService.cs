namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        string IdentityUserId { get; }
        string Email { get; }
        string PhoneNumber { get; }
        string ClientId { get; }
    }
}
