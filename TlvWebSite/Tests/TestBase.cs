using NUnit.Framework;
using OpenQA.Selenium;
using TlvWebSite.Utilities;
using TlvWebSite.Pages;

namespace TlvWebSite.Tests
{
    /// <summary>
    /// Base test class with WebDriver setup and teardown
    /// </summary>
    public abstract class TestBase
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected HomePage HomePage { get; private set; } = null!;

        /// <summary>
        /// Setup method runs before each test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            LoggerManager.Info($"Starting Test: {TestContext.CurrentContext.Test.Name}");
            // Create new driver instance for each test
            // Set to true for headless mode (CI/CD), false for visual debugging
            Driver = DriverFactory.CreateChromeDriver(headless: false);
            HomePage = new HomePage(Driver);
        }

        /// <summary>
        /// Teardown method runs after each test
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                // Capture screenshot on failure
                if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    LoggerManager.Error($"Test Failed: {TestContext.CurrentContext.Test.Name}", new Exception(TestContext.CurrentContext.Result.Message));
                    TakeScreenshot(TestContext.CurrentContext.Test.Name);
                }
                else
                {
                    LoggerManager.Info($"Test Finished: {TestContext.CurrentContext.Test.Name} with Status: {status}");
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error("Error during Teardown", ex);
            }
            finally
            {
                // Always close the browser
                Driver?.Quit();
                Driver?.Dispose();
            }
        }

        /// <summary>
        /// Take a screenshot and save it
        /// </summary>
        protected void TakeScreenshot(string testName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Screenshots", fileName);
                
                // Create screenshots directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                
                screenshot.SaveAsFile(filePath);
                TestContext.AddTestAttachment(filePath, $"Screenshot for {testName}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to capture screenshot: {ex.Message}");
            }
        }
    }
}
