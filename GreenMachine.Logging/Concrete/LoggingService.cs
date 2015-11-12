using System;
using GreenMachine.Interfaces.Logging;
using LogLevel = GreenMachine.Enums.LogLevel;
using NLog;

namespace GreenMachine.Logging.Concrete
{
    public class LoggingService : Logger, ILoggingService
    {
        public string LoggerName { get; set; }

        public static ILoggingService GetLoggingService()
        {
            ILoggingService logger = (ILoggingService) LogManager.GetLogger("NLogLogger", typeof (LoggingService));

            return logger;
        }

        public void Log(Exception exception)
        {
            Log(exception, "Exception");
        }

        public void Log(Exception exception, string message)
        {
            if (!IsLevelEnabled( LogLevel.Error))
                return;
            
            LogEvent(LogLevel.Error, message, exception.ToString());
        }

        public void Log(LogLevel level, string message, string formattedMessage)
        {
            if (!IsLevelEnabled(level))
                return;

            LogEvent(level, message, formattedMessage);
        }

        private void LogEvent(LogLevel level, string message, string formattedMessage)
        {
            var nLevel = ConvertToNLogLevel(level);

            var eventInfo = new LogEventInfo(nLevel, LoggerName, message);

            eventInfo.Properties["Severity"] = nLevel.ToString();
            eventInfo.Properties["Title"] = LoggerName;
            eventInfo.Properties["FormattedMessage"] = formattedMessage;

            base.Log(typeof(LoggingService), eventInfo);
        }

        /// <summary>
        /// Determine if the event being logged is turned on in the configuration
        /// </summary>
        private bool IsLevelEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return base.IsTraceEnabled;
                case LogLevel.Info:
                    return base.IsInfoEnabled;
                case LogLevel.Warn:
                    return base.IsWarnEnabled;
                case LogLevel.Error:
                    return base.IsErrorEnabled;
                case LogLevel.Fatal:
                    return base.IsFatalEnabled;
                default:
                    return false;
            }
        }

        private static NLog.LogLevel ConvertToNLogLevel(LogLevel level)
        {
            if (level == LogLevel.Trace)
                return NLog.LogLevel.Trace;
            if (level == LogLevel.Info)
                return NLog.LogLevel.Info;
            if (level == LogLevel.Warn)
                return NLog.LogLevel.Warn;
            if (level == LogLevel.Error)
                return NLog.LogLevel.Error;
            if (level == LogLevel.Fatal)
                return NLog.LogLevel.Fatal;

            return NLog.LogLevel.Trace;
        }
    }
}