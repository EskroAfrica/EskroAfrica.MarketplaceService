using EskroAfrica.MarketplaceService.Application;
using EskroAfrica.MarketplaceService.Application.Implementations;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.Models;
using EskroAfrica.MarketplaceService.Infrastructure.Data;
using EskroAfrica.MarketplaceService.Infrastructure.Implementations;
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

            // Add HttpCLientFactory
            services.AddHttpClient();

            // Add Kafka
            services.AddScoped<IKafkaProducerService, KafkaProducerService>();
            //services.AddHostedService<KafkaConsumerService>();

            // Add Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IPaystackService, PaystackService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
