using Core.Infrastructure.Interfaces.Logging;
using System;

namespace Infrastructure.NewRelic
{
    public class LoggingProvider : ILoggingProvider
    {
        public void LogError(string errorMessage, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string errorMessage, string dataString)
        {
            throw new NotImplementedException();
        }

        public void LogWarn(string errorMessage, string dataString)
        {
            throw new NotImplementedException();
        }
    }
}
