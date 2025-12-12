using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using MarsAdvancedTask.Framework.Models;
namespace MarsAdvancedTask.Framework.Helpers;

    public static class ExtentManager
    {
        private static ExtentReports? _extent;
        private static readonly object _lockObj= new();
        private static string? _reportPath;

        public static ExtentReports? GetExtentReports(Settings config)
        {
            if (_extent != null) return _extent;
            lock (_lockObj)
            {
                if (_extent != null) return _extent;
                {
                    var reportsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                    Directory.CreateDirectory(reportsDirectory);

                    // Build report file path (timestamped)
                    var timeStamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
                    var id = System.Diagnostics.Process.GetCurrentProcess().Id;  //unique id
                    _reportPath = Path.Combine(reportsDirectory, $"TestReport_{timeStamp}_p{id}.html");

                    var htmlReporter = new ExtentSparkReporter(_reportPath);
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
                    htmlReporter.Config.DocumentTitle = config.Report.Title;
                    htmlReporter.Config.ReportName = config.Report.Title;

                    var extent = new ExtentReports();
                    extent.AttachReporter(htmlReporter);

                    // System info
                    extent.AddSystemInfo("Environment", config.Environment.TestingEnvironment);
                    extent.AddSystemInfo("Tester", config.Environment.Tester);
                    extent.AddSystemInfo("OS", config.Environment.OS);
                    extent.AddSystemInfo("Browser", config.Browser.Type);
                    extent.AddSystemInfo("BaseUrl", config.Environment.BaseUrl);

                    _extent = extent;
                    return _extent;
                }
            }
        }

        public static void Flush()
        {
            lock (_lockObj)
            {
                _extent?.Flush();
            }
        }
    }

