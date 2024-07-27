using System.IO;
using Automation_task.Util;
using Automation_task.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Automation_task.Test
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
#pragma warning disable S2187
    public class BaseTest
    {
#pragma warning disable CS8618
        protected DriverManager _driverManager;
        protected IConfiguration _configuration;
        protected static ExtentReports _extent;
        private static ExtentSparkReporter _htmlReporter;
#pragma warning restore CS8618
        private static readonly object _lock = new();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string baseDrirectory = Path.Combine(Directory.GetCurrentDirectory(), "../../../");

            var builder = new ConfigurationBuilder()
                .SetBasePath(baseDrirectory + "Data/")
                .AddJsonFile("testData.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            // Initialing Logger
            Logger.ConfigLogger();

            Log.Information("initiating driver");
            _driverManager = new DriverManager();

            // Define the directory where you want to save the report
            string reportDirectory = Path.Combine(baseDrirectory, "Report");

            // Ensure the directory exists
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            // Specify the full path including the report file name
            string reportPath = Path.Combine(reportDirectory, "extent-report.html");

            lock (_lock)
            {
                if (_extent == null)
                {
                    // Initialize the ExtentHtmlReporter with the specified path
                    _extent = new();
                    _htmlReporter = new ExtentSparkReporter(reportPath);
                    _extent.AttachReporter(_htmlReporter);
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            lock (_lock)
            {
                _extent.Flush();
            }
            Log.Information("disposing driver");
            _driverManager!.Quit();
        }

    }
}