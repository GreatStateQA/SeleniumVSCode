using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using OpenQA.Selenium;

namespace VSCodeSelenium.Extensions {
    public class TestReportExtensions {
        private readonly IWebDriver _driver;
        public TestReportExtensions (IWebDriver driver) {
            _driver = driver;
        }
        public void ReportJsErrors () {
            var jsErrors = _driver.Manage ().Logs.GetLog (LogType.Browser);
            foreach (var jsError in jsErrors) {
                Console.Write (jsError);
                Debug.Write (jsError);

            }
        }

        /// <summary>
        /// This will take a full screen screenshot 
        /// </summary>
        /// <param name="rootpath"></param>
        /// <param name="imgName"></param>
        public void TakeScreenShot (string rootpath, string imgName) {
            Thread.Sleep (2000);
            if (_driver is ITakesScreenshot screenshotDriver) {
                var screenshot = screenshotDriver.GetScreenshot ();
                if (!Directory.Exists (rootpath)) {
                    Directory.CreateDirectory (rootpath);
                }
                screenshot.SaveAsFile ($"{rootpath}\\{imgName}.png", ScreenshotImageFormat.Png);
            }
        }
    }
}