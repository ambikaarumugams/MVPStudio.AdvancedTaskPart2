using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace MarsAdvancedTask.Framework.Pages.ManageRequests
{
    public class SentRequestComponent
    {
        private readonly TestState _state;
        public SentRequestComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _manageRequestsTabElement = By.XPath("//div[contains(@class,'dropdown') and contains(@class,'item') and normalize-space()='Manage Requests']");
        private readonly By _sentRequestsElement = By.XPath("//div[contains(@class,'menu') and contains(@class,'transition')]//a[normalize-space()='Sent Requests']");
        private readonly By _withDrawElement = By.XPath("//button[normalize-space()='Withdraw']");
        private readonly By _statusElement = By.XPath("//tbody/tr[1]/td[5]");
        private readonly By _tableElement = By.XPath("//table[@class='ui single line sortable striped table sortableHeader']//tr[1]//td");
        private readonly By _tableDataElementsFromColoumn2To5 = By.XPath("//table[@class='ui single line sortable striped table sortableHeader']//tr//td[position()=2 or position()=5]");

        //Action methods
        public void HoverOnElement()
        {
            var element = _state.Driver.FindElement(_manageRequestsTabElement);    //I got no such element exception
            Actions actions = new Actions(_state.Driver);
            actions.MoveToElement(element).Perform();
        }

        public void OpenSentRequests()
        {
            HoverOnElement();
            _state.Driver.FindElement(_sentRequestsElement).Click();
        }

        public string GetStatus()
        {
            var status = _state.Driver.FindElement(_statusElement);
            return status.Text;
        }

        public string GetDataFromTable()
        {
            var data = _state.Driver.FindElement(_tableDataElementsFromColoumn2To5);
            return data.Text;
        }

        public void ClickWithDraw()
        {
            _state.Driver.FindElement(_withDrawElement).Click();
        }
    }
}
