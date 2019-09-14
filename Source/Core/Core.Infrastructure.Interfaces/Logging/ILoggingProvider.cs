using System;

namespace Core.Infrastructure.Interfaces.Logging
{
    public interface ILoggingProvider
    {
        void LogInfo(string message);

        void LogTrace(string errorMessage, string dataString);

        void LogWarn(string errorMessage, string dataString);

        void LogError(string errorMessage, Exception ex);
    }
}
