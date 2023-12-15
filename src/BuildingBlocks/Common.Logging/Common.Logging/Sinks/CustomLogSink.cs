using Serilog.Core;
using Serilog.Events;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Common.Logging.Logs.BudgetLogs;
using Common.Logging.Logs.EntertainmentLogs;
using Common.Logging.Helpers;

namespace Common.Logging.Sinks
{
    public class CustomLogSink : ILogEventSink
    {
        private readonly HttpClient httpClient;
        private readonly string logControllerApiUrl = "https://localhost:8007/createBackUp";

        public CustomLogSink()
        {
            httpClient = new HttpClient();
        }

        public async void Emit(LogEvent logEvent)
        {
            var environment = logEvent.Properties["Environment"].ToString();
            var application = logEvent.Properties["Application"].ToString();

            var customLog = new CustomLog
            {
                LogMessage = logEvent.RenderMessage(),
                LogTime = logEvent.Timestamp.DateTime,
                Application = logEvent.Properties["Application"].ToString(),
                Level = logEvent.Level.ToString(),
                IsBackedUp = true
            };

            var json = JsonSerializer.Serialize(customLog);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (IsContaining(customLog.LogMessage))
                await httpClient.PostAsync(logControllerApiUrl, content);
        }

        private bool IsContaining(string logMessage)
        {
            foreach (string pattern in LogPatterns.GetLogPatterns())
                if (logMessage.Contains(pattern))
                    return true;
                
            return false;
        }
    }
}
