using OpenQA.Selenium;


namespace VSCodeSelenium.Models
{
    public class GooglePage_Model
    {

        private IWebDriver _driver;
        public GooglePage_Model(IWebDriver driver)
        {
            _driver = driver;
        }
      
        public IWebElement Link => _driver.FindElement(By.CssSelector("a.NKcBbd"));
    }
}