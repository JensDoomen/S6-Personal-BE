namespace Shop.Models
{
    public partial class EasyLogger : Attribute
    {
        [LoggerMessage(EventId = 100, Level = LogLevel.Critical, EventName = "CRITICAL", Message = "{message}")]
        public static partial void LogCritical(ILogger logger, string message, Exception ex);

        [LoggerMessage(EventId = 200, Level = LogLevel.Information, EventName = "INFO", Message = "{message}")]
        public static partial void LogInformation(ILogger logger, string message);
    }
}
