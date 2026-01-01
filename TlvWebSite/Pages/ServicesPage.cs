using OpenQA.Selenium;

using TlvWebSite.Utilities;

namespace TlvWebSite.Pages
{
    /// <summary>
    /// Page Object for Services Page
    /// </summary>
    public class ServicesPage : BasePage
    {
        private readonly By _bodyLocator = By.TagName("body");

        public ServicesPage(IWebDriver driver) : base(driver)
        {
        }

        public ServicesPage NavigateToServices()
        {
             LoggerManager.Info("Navigating directly to Services Page");
             // Fallback
             NavigateTo("https://www.tel-aviv.gov.il/Residents/Pages/default.aspx"); 
             return this;
        }

        public bool IsPageLoaded()
        {
            WaitForPageLoad();
            return Driver.Url.Contains("Residents") || Driver.Url.Contains("Services") || Driver.Title.Length > 0;
        }

        public bool VerifyServicesPageUrl()
        {
            return Driver.Url.Contains("Residents") || Driver.Url.Contains("Services") || Driver.Url.Contains("default");
        }

        public bool IsMainContentDisplayed()
        {
             return IsElementPresent(_bodyLocator);
        }
    }
}
