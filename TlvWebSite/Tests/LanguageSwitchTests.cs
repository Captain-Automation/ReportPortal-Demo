using NUnit.Framework;
using TlvWebSite.Pages;

namespace TlvWebSite.Tests
{
    /// <summary>
    /// Test class for Language switching functionality
    /// Test 5: Verify language switch from Hebrew to English
    /// </summary>
    [TestFixture]
    [Category("Language")]
    [Description("Tests for language switching on Tel Aviv Municipality website")]
    public class LanguageSwitchTests : TestBase
    {
        private const string EnglishUrlPath = "/en";
        private const string EnglishPageTitle = "Tel Aviv";

        /// <summary>
        /// Test 5: Verify switching from Hebrew to English
        /// </summary>
        [Test]
        [Description("Verify language switch from Hebrew to English works correctly")]
        public void VerifyLanguageSwitch()
        {
            // Arrange - Start on Hebrew homepage
            HomePage.NavigateToHome();
            var initialUrl = HomePage.GetCurrentUrl();
            var initialTitle = HomePage.GetPageTitle();
            
            TestContext.WriteLine($"Initial Hebrew page URL: {initialUrl}");
            TestContext.WriteLine($"Initial Hebrew page title: {initialTitle}");

            // Verify we start in Hebrew
            Assert.That(
                initialTitle.Contains("תל-אביב") || initialTitle.Contains("תל אביב") || initialTitle.Contains("עירייה"),
                Is.True,
                $"Should start on Hebrew page. Title: {initialTitle}");

            // Act - Navigate to English version
            Driver.Navigate().GoToUrl("https://www.tel-aviv.gov.il/en");
            
            // Wait for page load
            System.Threading.Thread.Sleep(2000);

            // Assert - Verify URL changed to English
            var englishUrl = HomePage.GetCurrentUrl();
            Assert.That(
                englishUrl.Contains(EnglishUrlPath, StringComparison.OrdinalIgnoreCase),
                Is.True,
                $"URL should contain '/en' for English version. Actual URL: {englishUrl}");

            // Assert - Verify page title is in English
            var englishTitle = HomePage.GetPageTitle();
            Assert.That(
                englishTitle.Contains(EnglishPageTitle, StringComparison.OrdinalIgnoreCase),
                Is.True,
                $"Page title should contain 'Tel Aviv' in English. Actual title: {englishTitle}");

            // Log success
            TestContext.WriteLine($"Successfully switched to English");
            TestContext.WriteLine($"English page URL: {englishUrl}");
            TestContext.WriteLine($"English page title: {englishTitle}");
        }

        /// <summary>
        /// Verify English page is properly displayed
        /// </summary>
        [Test]
        [Description("Verify English homepage displays correctly")]
        public void VerifyEnglishHomePageDisplays()
        {
            // Arrange & Act - Navigate directly to English page
            Driver.Navigate().GoToUrl("https://www.tel-aviv.gov.il/en");
            
            // Wait for page load
            System.Threading.Thread.Sleep(2000);

            // Assert
            var currentUrl = HomePage.GetCurrentUrl();
            var pageTitle = HomePage.GetPageTitle();

            Assert.That(
                currentUrl.Contains("/en"),
                Is.True,
                $"Should be on English page. URL: {currentUrl}");

            Assert.That(
                pageTitle.Contains("Tel Aviv", StringComparison.OrdinalIgnoreCase),
                Is.True,
                $"English page title should contain 'Tel Aviv'. Actual: {pageTitle}");

            // Verify page content loaded
            Assert.That(HomePage.IsHeaderDisplayed() || HomePage.IsLogoDisplayed(), Is.True,
                "English homepage should display header or logo");

            TestContext.WriteLine($"English homepage verified successfully");
            TestContext.WriteLine($"URL: {currentUrl}");
            TestContext.WriteLine($"Title: {pageTitle}");
        }

        /// <summary>
        /// Verify switching back from English to Hebrew
        /// </summary>
        [Test]
        [Description("Verify switching from English back to Hebrew")]
        public void VerifySwitchFromEnglishToHebrew()
        {
            // Arrange - Start on English page
            Driver.Navigate().GoToUrl("https://www.tel-aviv.gov.il/en");
            System.Threading.Thread.Sleep(2000);
            
            var englishUrl = HomePage.GetCurrentUrl();
            TestContext.WriteLine($"Starting on English page: {englishUrl}");

            // Act - Navigate to Hebrew version
            Driver.Navigate().GoToUrl("https://www.tel-aviv.gov.il");
            System.Threading.Thread.Sleep(2000);

            // Assert
            var hebrewUrl = HomePage.GetCurrentUrl();
            var hebrewTitle = HomePage.GetPageTitle();

            Assert.That(
                !hebrewUrl.Contains("/en"),
                Is.True,
                $"Hebrew URL should not contain '/en'. Actual: {hebrewUrl}");

            Assert.That(
                hebrewTitle.Contains("תל-אביב") || hebrewTitle.Contains("תל אביב"),
                Is.True,
                $"Hebrew page title should contain Hebrew text. Actual: {hebrewTitle}");

            TestContext.WriteLine($"Successfully switched back to Hebrew");
            TestContext.WriteLine($"Hebrew URL: {hebrewUrl}");
            TestContext.WriteLine($"Hebrew title: {hebrewTitle}");
        }
    }
}
