using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TlvWebSite.Utilities
{
    /// <summary>
    /// Factory class for creating WebDriver instances
    /// </summary>
    public static class DriverFactory
    {
        /// <summary>
        /// Create a new Chrome WebDriver instance
        /// </summary>
        /// <param name="headless">Run browser in headless mode</param>
        /// <returns>IWebDriver instance</returns>
        public static IWebDriver CreateChromeDriver(bool headless = false)
        {
            var options = new ChromeOptions();
            
            // Common options for stability
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            
            // Handle Hebrew/RTL content
            options.AddArgument("--lang=he-IL");
            
            if (headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
            }

            var driver = new ChromeDriver(options);
            
            // Set implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            
            return driver;
        }

        /// <summary>
        /// Create a Chrome driver with custom options
        /// </summary>
        public static IWebDriver CreateChromeDriver(ChromeOptions customOptions)
        {
            var driver = new ChromeDriver(customOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            return driver;
        }
    }
}
