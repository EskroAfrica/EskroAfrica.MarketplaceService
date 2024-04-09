using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface ILogService
    {
        void LogEvent(LogEventLevel level, string message, params object?[]? propertyValues);
    }
}
