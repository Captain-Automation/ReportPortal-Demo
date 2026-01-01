using OpenQA.Selenium;

namespace TlvWebSite.Pages
{
    /// <summary>
    /// Page Object for Tel Aviv Municipality Search Results Page
    /// </summary>
    public class SearchResultsPage : BasePage
    {
        // Locators
        private readonly By _searchResultsLocator = By.CssSelector(".search-results, .results-list, [class*='search-result'], #search-results");
        private readonly By _searchResultItemLocator = By.CssSelector(".search-result-item, .result-item, .search-result a, [class*='result-item']");
        private readonly By _searchBoxLocator = By.CssSelector("input[type='search'], input[type='text'][name*='search'], .search-input");
        private readonly By _noResultsLocator = By.CssSelector(".no-results, .empty-results, [class*='no-result']");
        private readonly By _resultsCountLocator = By.CssSelector(".results-count, .search-count, [class*='result-count']");
        private readonly By _pageHeaderLocator = By.CssSelector("h1, .page-title, .search-header");
        private readonly By _mainContentLocator = By.CssSelector("main, .main-content, #content, .page-content");
        private readonly By _paginationLocator = By.CssSelector(".pagination, .pager, [class*='pagination']");

        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Check if search results are displayed
        /// </summary>
        public bool AreResultsDisplayed()
        {
            try
            {
                WaitForPageLoad();
                // Check if either results container or result items exist
                return IsElementPresent(_searchResultsLocator) || 
                       Driver.FindElements(_searchResultItemLocator).Count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the count of search result items
        /// </summary>
        public int GetResultsCount()
        {
            try
            {
                var results = Driver.FindElements(_searchResultItemLocator);
                return results.Count;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Check if no results message is displayed
        /// </summary>
        public bool IsNoResultsDisplayed()
        {
            return IsElementPresent(_noResultsLocator);
        }

        /// <summary>
        /// Get text from results count element
        /// </summary>
        public string GetResultsCountText()
        {
            try
            {
                return GetText(_resultsCountLocator);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the current search term from the search box
        /// </summary>
        public string GetCurrentSearchTerm()
        {
            try
            {
                var searchBox = Driver.FindElement(_searchBoxLocator);
                return searchBox.GetAttribute("value") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Perform a new search from the results page
        /// </summary>
        public SearchResultsPage NewSearch(string searchTerm)
        {
            var searchBox = WaitForElement(_searchBoxLocator);
            searchBox.Clear();
            searchBox.SendKeys(searchTerm);
            searchBox.SendKeys(Keys.Enter);
            WaitForPageLoad();
            return this;
        }

        /// <summary>
        /// Check if page header is displayed
        /// </summary>
        public bool IsPageHeaderDisplayed()
        {
            try
            {
                return WaitForElement(_pageHeaderLocator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Click on a specific search result by index
        /// </summary>
        public void ClickResultByIndex(int index)
        {
            var results = Driver.FindElements(_searchResultItemLocator);
            if (results.Count > index)
            {
                results[index].Click();
                WaitForPageLoad();
            }
        }

        /// <summary>
        /// Check if pagination is present
        /// </summary>
        public bool IsPaginationPresent()
        {
            return IsElementPresent(_paginationLocator);
        }

        /// <summary>
        /// Verify the URL contains search-related path
        /// </summary>
        public bool VerifySearchResultsUrl()
        {
            var currentUrl = GetCurrentUrl();
            return currentUrl.Contains("search", StringComparison.OrdinalIgnoreCase) ||
                   currentUrl.Contains("חיפוש") ||
                   currentUrl.Contains("q=") ||
                   currentUrl.Contains("query=");
        }

        /// <summary>
        /// Check if main content is displayed
        /// </summary>
        public bool IsMainContentDisplayed()
        {
            try
            {
                return WaitForElement(_mainContentLocator).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
