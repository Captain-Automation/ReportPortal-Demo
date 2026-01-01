using NUnit.Framework;
using TlvWebSite.Pages;

namespace TlvWebSite.Tests
{
    /// <summary>
    /// Test class for Tel Aviv Municipality Homepage
    /// Test 1: Verify homepage loads correctly
    /// </summary>
    [TestFixture]
    [Category("HomePage")]
    [Description("Tests for Tel Aviv Municipality Homepage")]
    public class HomePageTests : TestBase
    {
        /// <summary>
        /// Test 1: Verify that the homepage loads correctly
        /// Checks: Page title, header/logo display, and main navigation presence
        /// </summary>
        [Test]
        [Description("Verify homepage loads with correct title, logo, and navigation")]
        public void VerifyHomePageLoads()
        {
            // Arrange & Act
            HomePage.NavigateToHome();

            // Assert - Verify page title contains Tel Aviv (Hebrew or English)
            var pageTitle = HomePage.GetPageTitle();
            Assert.That(
                pageTitle.Contains("תל-אביב") || 
                pageTitle.Contains("תל אביב") || 
                pageTitle.Contains("Tel Aviv", StringComparison.OrdinalIgnoreCase),
                Is.True,
                $"Page title should contain 'תל אביב' or 'Tel Aviv'. Actual title: {pageTitle}");

            // Assert - Verify header or logo is displayed
            var isHeaderOrLogoDisplayed = HomePage.IsHeaderDisplayed() || HomePage.IsLogoDisplayed();
            Assert.That(isHeaderOrLogoDisplayed, Is.True, 
                "Homepage header or logo should be displayed");

            // Assert - Verify main navigation is present
            var isNavPresent = HomePage.IsMainNavigationPresent();
            Assert.That(isNavPresent, Is.True, 
                "Main navigation should be present on homepage");

            // Log success
            TestContext.WriteLine($"Homepage loaded successfully. Title: {pageTitle}");
            TestContext.WriteLine($"Current URL: {HomePage.GetCurrentUrl()}");
        }

        /// <summary>
        /// Verify that the homepage is displayed in Hebrew by default
        /// </summary>
        [Test]
        [Description("Verify homepage displays in Hebrew by default")]
        public void VerifyHomePageIsInHebrew()
        {
            // Arrange & Act
            HomePage.NavigateToHome();

            // Assert
            var pageTitle = HomePage.GetPageTitle();
            var currentUrl = HomePage.GetCurrentUrl();

            Assert.That(
                HomePage.IsPageInHebrew(),
                Is.True,
                $"Homepage should be in Hebrew by default. Title: {pageTitle}, URL: {currentUrl}");

            TestContext.WriteLine($"Homepage is correctly displayed in Hebrew. Title: {pageTitle}");
        }
    }
}
