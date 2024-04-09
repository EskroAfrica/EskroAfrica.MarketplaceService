using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.Models;
using Serilog;
using Serilog.Events;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class LogService : ILogService
    {
        private readonly AppSettings _appSettings;

        public LogService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void LogEvent(LogEventLevel level, string message, params object?[]? propertyValues)
        {
            message = $"{_appSettings.LogSettings.LogRef}: {message}";
            Log.Write(level, message, propertyValues);
        }
    }
}
