using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using VSCodeSelenium.Extensions;

namespace VSCodeSelenium {
    public class BaseClass {
        public IWebDriver Driver { get; private set; }
            public BaseClass()
        {            
   /*          var Test = 
            BaseUrl = System.ConfigurationManager.AppSettings["baseURL"];
            Browser = ConfigurationManager.AppSettings["browser"];
            BlankUrl = ConfigurationManager.AppSettings["blankUrl"];
            WaitTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["waitTimeOut"]);
            BrowserstackUser = ConfigurationManager.AppSettings["browserstackUser"];
            BrowserstackKey = ConfigurationManager.AppSettings["browserstackKey"]; */
        }
        #region App settings
        private TestReportExtensions _ext;
        public string BaseUrl { get; set; }
        public void URL(){
            BaseUrl = "https://www.google.co.uk";
        }
        public string Browser { get; set; }
        public void browser(){
            Browser = "chrome";       
        }
        public string BlankUrl { get; set; }
        public int WaitTimeout { get; set; }
        public string BrowserstackUser { get; set; }
        public string BrowserstackKey { get; set; }
        #endregion
        /// <summary>
        /// This will initialise the browser declared in the app.config
        /// </summary>
        public void InitialiseBrowser () {
            switch (Browser) {
                ////Selenium - Chrome
                case "chrome":
                    var outPutDirectory = Path.GetDirectoryName (Assembly.GetExecutingAssembly().Location);
                    var seleniumChromeOptions = new ChromeOptions ();
                    seleniumChromeOptions.AddArgument ("--start-maximized");
                     seleniumChromeOptions.AddArguments("--no-sandbox", "--disable-gpu");
                     seleniumChromeOptions.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application"; 
                    //seleniumChromeOptions.BinaryLocation = outPutDirectory;
                    //SeleniumChromeOptions.AddArgument("--window-size=1366,728");
                    seleniumChromeOptions.SetLoggingPreference (LogType.Browser, LogLevel.All);
                    // Driver = new ChromeDriver (@"C:\working_git\VSCodeSelenium\bin\Debug\netcoreapp2.0\", seleniumChromeOptions);
Driver = new RemoteWebDriver(new Uri("http://localhost:9515"), seleniumChromeOptions);
                    break;

                case "edge":
                    //Environment.SetEnvironmentVariable("webdriver.edge.driver", @"/bin/chromedriver.exe");
                    var options = new EdgeOptions ();
                    Driver = new RemoteWebDriver (new Uri ("http://localhost:17556"), options);
                    break;

                    //Selenium remote
                case "remote":
                    var capability = new DesiredCapabilities ();
                    capability.SetCapability ("browser", "Edge");
                    capability.SetCapability ("browser_version", "16.0");
                    capability.SetCapability ("os", "Windows");
                    capability.SetCapability ("os_version", "10");
                    capability.SetCapability ("resolution", "1366x768");
                    capability.SetCapability ("browserstack.user", BrowserstackUser);
                    capability.SetCapability ("browserstack.key", BrowserstackKey);
                    Driver = new RemoteWebDriver (
                        new Uri ("http://hub-cloud.browserstack.com/wd/hub/"), capability);
                    break;
            }

            // set implicit wait
            Driver.Manage ().Timeouts ().ImplicitWait = TimeSpan.FromSeconds (5);
            _ext = new TestReportExtensions (Driver);
        }

        [TearDown]
        public void TeardownTest () {
            Driver.Quit ();
        }

    }

}