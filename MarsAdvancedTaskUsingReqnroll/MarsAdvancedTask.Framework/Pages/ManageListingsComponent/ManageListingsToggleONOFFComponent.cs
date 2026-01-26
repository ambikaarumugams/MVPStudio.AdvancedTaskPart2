using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages.ManageListingsComponent
{

    public class ManageListingsToggleONOFFComponent
    {
        private readonly TestState _state;

        public ManageListingsToggleONOFFComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _manageListingsLink = By.XPath("//a[normalize-space()='Manage Listings']");
        private readonly By _manageListingsTable = By.XPath("//table[@class='ui striped table']");
        //input[@name='isActive']

        //Action methods
        public void ClickManageListingsLink()
        {
            var manageListingsElement = _state.Wait.WaitUntilElementToBeClickable(_manageListingsLink);
            manageListingsElement.Click();
        }

        public void ClickToggle()
        {
            var manageListingsTable = _state.Driver.FindElements(_manageListingsTable);
            foreach (var element in manageListingsTable)
            {
                var toggle = element.FindElement(By.XPath("//table[@class='ui striped table']//tr[1]/td[7]"));
                toggle.Click();
            }
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
