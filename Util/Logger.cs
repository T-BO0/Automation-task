using System.IO;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Automation_task.Util
{
    public static class Logger
    {
        public static void ConfigLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Grayscale)
                .WriteTo.File(
                    Path.Combine(Directory.GetCurrentDirectory(), "../../../Logs/Logs.log"),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}