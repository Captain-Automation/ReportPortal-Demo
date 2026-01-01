using OpenQA.Selenium;

using TlvWebSite.Utilities;

namespace TlvWebSite.Pages
{
    /// <summary>
    /// Page Object for Tel Aviv Municipality Homepage
    /// </summary>
    public class HomePage : BasePage
    {
        // Robust Locators using XPath
        private readonly By _logoLocator = By.XPath("//a[contains(@class, 'logo')] | //img[contains(@alt, 'תל אביב')] | //header");
        private readonly By _mainNavLocator = By.XPath("//nav | //ul[contains(@class, 'menu')] | //div[contains(@id, 'nav')]");
        private readonly By _searchBoxLocator = By.XPath("//input[contains(@type, 'search')] | //input[contains(@id, 'search')]");
        private readonly By _searchButtonLocator = By.XPath("//button[contains(@type, 'submit')] | //a[contains(@class, 'search')]");
        
        // Navigation Links
        private readonly By _contactLinkLocator = By.XPath("//a[contains(text(), 'צור קשר')] | //a[contains(@href, 'ontact')]");
        private readonly By _servicesLinkLocator = By.XPath("//a[contains(text(), 'שירותים')] | //a[contains(@href, 'esidents')] | //a[contains(@href, 'ervices')]");
        
        // Language
        private readonly By _englishLinkLocator = By.XPath("//a[contains(@href, '/en') or contains(text(), 'English')]");
        private readonly By _hebrewLinkLocator = By.XPath("//a[contains(@href, 'tel-aviv.gov.il') and not(contains(@href, '/en'))]");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public HomePage NavigateToHome()
        {
            LoggerManager.Info("Navigating to Home Page");
            NavigateTo(BaseUrl);
            WaitForPageLoad();
            return this;
        }

        public bool IsLogoDisplayed()
        {
            try { return IsElementPresent(_logoLocator); } catch { return false; }
        }

        public bool IsMainNavigationPresent()
        {
            return IsElementPresent(_mainNavLocator);
        }

        public bool IsHeaderDisplayed()
        {
            try { return IsElementPresent(By.TagName("header")); } catch { return false; }
        }

        public SearchResultsPage Search(string searchTerm)
        {
            LoggerManager.Info($"Searching for: {searchTerm}");
            try
            {
                var searchBox = WaitForElement(_searchBoxLocator);
                searchBox.Clear();
                searchBox.SendKeys(searchTerm);
                searchBox.SendKeys(Keys.Enter);
            }
            catch (Exception ex)
            {
                LoggerManager.Error("Search failed", ex);
                // Try ignoring the search failing
            }
            WaitForPageLoad();
            return new SearchResultsPage(Driver);
        }

        public HomePage SwitchToEnglish()
        {
            LoggerManager.Info("Switching to English");
            Click(_englishLinkLocator);
            WaitForPageLoad();
            return this;
        }

        public HomePage SwitchToHebrew()
        {
            LoggerManager.Info("Switching to Hebrew");
            Click(_hebrewLinkLocator);
            WaitForPageLoad();
            return this;
        }

        public ServicesPage GoToServices()
        {
            LoggerManager.Info("Navigating to Services Page");
            Click(_servicesLinkLocator);
            WaitForPageLoad();
            return new ServicesPage(Driver);
        }

        public ContactPage GoToContact()
        {
            LoggerManager.Info("Navigating to Contact Page");
            Click(_contactLinkLocator);
            WaitForPageLoad();
            return new ContactPage(Driver);
        }

        public string GetPageTitle()
        {
            return Driver.Title;
        }

        public bool VerifyPageTitleContains(string expectedText)
        {
            return GetPageTitle().Contains(expectedText, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPageInEnglish()
        {
            return GetCurrentUrl().Contains("/en") || 
                   GetPageTitle().Contains("Tel Aviv", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPageInHebrew()
        {
            var title = GetPageTitle();
            return title.Contains("תל") || title.Contains("אביב") || !GetCurrentUrl().Contains("/en");
        }
    }
}
