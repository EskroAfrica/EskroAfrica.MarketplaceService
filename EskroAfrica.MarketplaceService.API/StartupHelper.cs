using EskroAfrica.MarketplaceService.Application;
using EskroAfrica.MarketplaceService.Common.Models;
using EskroAfrica.MarketplaceService.Infrastructure.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace EskroAfrica.MarketplaceService.API
{
    public static class StartupHelper
    {
        public static void AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
            // Bind AppSettings
            services.Configure<AppSettings>(config.GetSection(nameof(AppSettings)));
            services.AddScoped(config => config.GetRequiredService<IOptions<AppSettings>>().Value);

            var appSettings = services.BuildServiceProvider().GetService<AppSettings>();

            // AddDbContext
            services.AddDbContext<MarketplaceServiceDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Add Authentication
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = appSettings.IdentitySettings.Authority;
                    options.TokenValidationParameters.ValidateAudience = appSettings.IdentitySettings.ValidateAudience;
                });
            services.AddAuthorization();

            // Add Hangfire
            services.AddHangfire(options => options.UseSqlServerStorage(config.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            // Add Automapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
        }
    }
}
