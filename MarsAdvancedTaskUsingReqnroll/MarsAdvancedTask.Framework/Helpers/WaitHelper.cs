using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedTask.Framework.Helpers
{
    public class WaitHelper
    {
        private IWebDriver Driver { get; }
        private readonly WebDriverWait _wait;

        public WaitHelper(IWebDriver driver,int timeoutSeconds=10)
        {
            Driver = driver;
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        public IWebElement WaitUntilElementToBeClickable(By locator)
        {
            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }

            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Element is not clickable:{locator}");
            }
        }

        public IWebElement WaitUntilElementIsVisible(By locator)
        {
            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }

            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Element is not visible:{locator}");
            }
        }

        public bool WaitUntilInvisibilityOfElementLocated(By locator)
        {
            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Element not clickable or not found: {locator}");
            }
        }

        public void SafeClick(By locator, int maxRetries = 3)
        {
            for(int attempt=0;attempt<maxRetries;attempt++)
            {
                try
                {
                    var element = WaitUntilElementToBeClickable(locator);
                    element.Click();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    if (attempt == maxRetries - 1) throw;
                    Thread.Sleep(500);
                }
            }
        }

        public void SafeSendKeys(By locator, string text, int maxRetries = 3)
        {
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    var element = WaitUntilElementIsVisible(locator);
                    element.Clear();
                    element.SendKeys(text);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    if (attempt == maxRetries - 1) throw;
                    Thread.Sleep(500);
                }
            }
        }

        public string SafeGetText(By locator, int maxRetries = 3)
        {
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    var element = WaitUntilElementIsVisible(locator);
                    return element.Text;
                }
                catch (StaleElementReferenceException)
                {
                    if (attempt == maxRetries - 1) throw;
                    Thread.Sleep(500);
                }
            }
            return string.Empty;
        }
    }
}
