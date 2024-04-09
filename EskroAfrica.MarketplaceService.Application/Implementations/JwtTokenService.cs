using EskroAfrica.MarketplaceService.Application.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string FirstName => GetClaim(JwtClaimTypes.GivenName);
        public string LastName => GetClaim(JwtClaimTypes.FamilyName);
        public string FullName => GetClaim(JwtClaimTypes.Name);
        public string IdentityUserId => GetClaim(JwtClaimTypes.Subject);
        public string Email => GetClaim(JwtClaimTypes.Email);
        public string PhoneNumber => GetClaim(JwtClaimTypes.PhoneNumber);
        public string ClientId => GetClaim(JwtClaimTypes.ClientId);

        private string GetClaim(string claimType)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            return claims?.FirstOrDefault(c => c.Type == claimType).Value;
        }
    }
}
