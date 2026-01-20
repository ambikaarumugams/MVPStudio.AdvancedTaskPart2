using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages.ManageListingsComponent
{
    public class ManageListingsDeleteComponent
    {
        private readonly TestState _state;

        public ManageListingsDeleteComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _manageListingsLink = By.XPath("//a[normalize-space()='Manage Listings']");
        private readonly By _manageListingsTable = By.XPath("//table[@class='ui striped table']");

        //Action methods
        public void ClickManageListingsLink()
        {
            var manageListingsElement = _state.Wait.WaitUntilElementToBeClickable(_manageListingsLink);
            manageListingsElement.Click();
        }

        public void DeleteSpecificSharedSkill(string title)
        {
            ClickManageListingsLink();
            var table = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTable);
            var row = table.FindElement(By.XPath($".//tbody/tr[td[3][normalize-space(text())='{title}']]")); // find the row where 3rd column text matches the title
            var deleteButton = row.FindElement(By.XPath(".//button[i[contains(@class,'remove icon')]]")); // find the delete button in that row
            deleteButton.Click();
            var deleteYesButton = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//button[@class='ui icon positive right labeled button']"));
            deleteYesButton.Click();
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

        public bool IsSkillPresent()
        {
            try
            {
                var manageListingsTable = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTable);
                var rows = manageListingsTable.FindElements(By.XPath("//tbody//tr"));
                // return rows.Any(r => r.Text.Contains(title));
                return rows.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
