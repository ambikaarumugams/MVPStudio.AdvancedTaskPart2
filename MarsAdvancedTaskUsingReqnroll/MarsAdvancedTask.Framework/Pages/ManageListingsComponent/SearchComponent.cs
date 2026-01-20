using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages.ManageListingsComponent
{
    public class SearchComponent
    {
        private readonly TestState _state;
        public SearchComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _searchIconElement = By.XPath("//div[@class='ui secondary menu']//i[@class='search link icon']");
        private readonly By _searchTextBoxElement = By.XPath("//div[@class='ui secondary menu']//input[@placeholder='Search skills']");
        private readonly By _allCategoriesList = By.XPath("//div[@role='list']//a");
        private readonly By _searchSkillsTableElements = By.XPath("//div[@class='ui stackable three cards']//div//a[@class='service-info']");
        private readonly By _sellerInfoElement = By.XPath("//div[@class='ui stackable three cards']//div[1]//div[1]//a[1]");
        private readonly By _yesButton = By.XPath("//button[normalize-space()='Yes']");
        private readonly By _noButton = By.XPath("//button[normalize-space()='No']");
        private By SearchResultByCategory(string category) => By.XPath($"//section[contains(@class,'search-results')]//div[contains(@class,'ui card')][.//*[contains(normalize-space(),'{category}')]]");

        //Action methods
        public void SearchByKeyword(string category)
        {
            var search = _state.Driver.FindElement(_searchTextBoxElement);
            search.Clear();
            search.SendKeys(category);
            _state.Driver.FindElement(_searchIconElement).Click();
        }

        public void OpenListingByTitle(string categoryName)
        {
            var categories = _state.Driver.FindElements(_allCategoriesList);

            foreach (var category in categories)
            {
                if (category.Text.Contains(categoryName))
                {
                    category.Click();
                    return;
                }
            }
            throw new Exception($"Listing not found: {categoryName}");
        }

        public void ClickListingTitle(string title)
        {
            var searchSkillTable = _state.Driver.FindElements(_searchSkillsTableElements);

            foreach (var row in searchSkillTable)
            {
                var listing = row.FindElement(
                    By.XPath($".//p[contains(@class,'row-padded') and contains(normalize-space(), '{title}')]")
                );

                listing.Click();
                return;
            }

            throw new Exception($"Listing not found: {title}");
        }

        public void ClickYesButton()
        {
            _state.Driver.FindElement(_yesButton).Click();
        }

        public void ClickNoButton()
        {
            _state.Driver.FindElement(_noButton).Click();
        }

        public void ClickSellerInfo()
        {
            var sellerInfo = _state.Driver.FindElement(_sellerInfoElement);
            sellerInfo.Click();
        }

        public void ClickTheServiceListing()
        {
            var serviceListing = _state.Driver.FindElement(By.XPath("//div[@class='ui three cards']//div[1]//div[1]//a[2]//p[1]"));
            serviceListing.Click();
        }

        public string GetSuccessMessage() //Get success Message
        {
            try
            {
                var successMessage = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
                return successMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool IsListingVisibleInSearch(string category)
        {
            _state.Driver.FindElement(_searchTextBoxElement).Clear();
            _state.Driver.FindElement(_searchTextBoxElement).SendKeys(category);
            _state.Driver.FindElement(_searchIconElement).Click();

            // Use your WaitHelper here if available
            var results = _state.Driver.FindElements(SearchResultByCategory(category));
            return results.Any();
        }
    }
}
