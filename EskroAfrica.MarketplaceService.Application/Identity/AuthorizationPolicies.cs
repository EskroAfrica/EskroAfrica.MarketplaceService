using Microsoft.AspNetCore.Authorization;

namespace EskroAfrica.MarketplaceService.Application.Identity
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationPolicy CanAccessApp()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        }

        public static AuthorizationPolicy CanAccessBackOffice()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole("Admin")
                .Build();
        }
    }
}
