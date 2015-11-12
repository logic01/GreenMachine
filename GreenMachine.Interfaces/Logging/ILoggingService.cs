using GreenMachine.Enums;
using System;

namespace GreenMachine.Interfaces.Logging
{
    public interface ILoggingService
    {
        void Log(Exception exception);

        void Log(Exception exception, string message);

        void Log(LogLevel level, string message, string formattedMessage);
    }
}