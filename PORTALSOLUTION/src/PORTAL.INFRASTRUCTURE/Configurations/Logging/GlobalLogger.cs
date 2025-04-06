using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.INFRASTRUCTURE.Configurations.Logging
{
    public interface IGlobalLogger
    {
        void LogInformation(string message);
        void LogError(string message, Exception exception);
        void LogWarning(string message);
        // Add more methods for other log levels if needed
    }

    public class GlobalLogger : IGlobalLogger
    {
        private readonly ILogger<GlobalLogger> _logger;

        public GlobalLogger(ILogger<GlobalLogger> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.LogError(exception, message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
