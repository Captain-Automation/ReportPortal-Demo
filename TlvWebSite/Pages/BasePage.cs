using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TlvWebSite.Pages
{
    /// <summary>
    /// Base page class containing common functionality for all page objects
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;
        protected const string BaseUrl = "https://www.tel-aviv.gov.il";
        protected const int DefaultTimeoutSeconds = 15;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultTimeoutSeconds));
        }

        /// <summary>
        /// Wait for element to be visible and return it
        /// </summary>
        protected IWebElement WaitForElement(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// Wait for element to be clickable and return it
        /// </summary>
        protected IWebElement WaitForClickable(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        /// <summary>
        /// Check if element exists on the page
        /// </summary>
        protected bool IsElementPresent(By locator)
        {
            try
            {
                Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the current page title
        /// </summary>
        public string GetPageTitle()
        {
            return Driver.Title;
        }

        /// <summary>
        /// Get the current URL
        /// </summary>
        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        /// <summary>
        /// Navigate to a specific URL
        /// </summary>
        protected void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Click on an element
        /// </summary>
        protected void Click(By locator)
        {
            WaitForClickable(locator).Click();
        }

        /// <summary>
        /// Enter text into an input field
        /// </summary>
        protected void SendKeys(By locator, string text)
        {
            var element = WaitForElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Get text from an element
        /// </summary>
        protected string GetText(By locator)
        {
            return WaitForElement(locator).Text;
        }

        /// <summary>
        /// Wait for page to fully load
        /// </summary>
        protected void WaitForPageLoad()
        {
            Wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }

        /// <summary>
        /// Scroll to an element
        /// </summary>
        protected void ScrollToElement(By locator)
        {
            var element = Driver.FindElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}
