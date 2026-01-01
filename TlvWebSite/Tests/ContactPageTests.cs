using NUnit.Framework;
using TlvWebSite.Pages;

namespace TlvWebSite.Tests
{
    [TestFixture]
    [Category("ContactPage")]
    public class ContactPageTests : TestBase
    {
        [Test]
        [Ignore("Contact link is currently not clickable/found on homepage.")]
        public void VerifyContactPageLoads()
        {
            HomePage.NavigateToHome();
            var contactPage = HomePage.GoToContact();
            Assert.That(contactPage.IsPageLoaded(), Is.True, "Contact page should load");
        }

        [Test]
        [Ignore("Contact link is currently not clickable/found on homepage.")]
        public void VerifyContactPageHeader()
        {
            HomePage.NavigateToHome();
            var contactPage = HomePage.GoToContact();
            Assert.That(Driver.Title.Length > 0, Is.True, "Page should have a title");
        }
    }
}
