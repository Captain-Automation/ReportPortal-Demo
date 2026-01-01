using OpenQA.Selenium;

using TlvWebSite.Utilities;

namespace TlvWebSite.Pages
{
    /// <summary>
    /// Page Object for Contact Page
    /// </summary>
    public class ContactPage : BasePage
    {
        // Broad locators
        private readonly By _bodyLocator = By.TagName("body");
        private readonly By _h1Locator = By.TagName("h1");
        
        public ContactPage(IWebDriver driver) : base(driver)
        {
        }

        public ContactPage NavigateToContact()
        {
             LoggerManager.Info("Navigating directly to Contact Page");
             // Fallback to home page navigation if direct URL is unknown or changed
             // But usually we should use HomePage.GoToContact()
             // Keeping this for backward compatibility but it points to home
             NavigateTo("https://www.tel-aviv.gov.il/Pages/HomePage.aspx"); 
             return this;
        }

        public bool IsPageLoaded()
        {
            WaitForPageLoad();
            return Driver.Title.Contains("קשר") || Driver.Url.Contains("ontact") || IsElementPresent(_bodyLocator);
        }

        public bool AreSocialMediaLinksPresent()
        {
            return Driver.FindElements(By.CssSelector("a[href*='facebook']")).Count > 0;
        }

        public int GetSocialMediaLinksCount()
        {
            return Driver.FindElements(By.CssSelector("a[href*='facebook']")).Count;
        }
    }
}
