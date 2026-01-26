using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace MarsAdvancedTask.Framework.Pages.ManageRequests
{
    public class ReceiveRequestComponent
    {
        private readonly TestState _state;
        public ReceiveRequestComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _manageRequestsTabElement = By.XPath("//div[contains(@class,'ui') and contains(@class,'dropdown') and contains(@class,'item') and normalize-space()='Manage Requests']");
        private readonly By _receivedRequestsElement = By.XPath("//div[contains(@class,'menu') and contains(@class,'transition')]//a[normalize-space()='Received Requests']");
        private readonly By _statusElement = By.XPath("//tbody/tr[1]/td[5]");
        private readonly By _tableElement = By.XPath("//table[@class='ui single line sortable striped table sortableHeader']//tr[1]//td");
        private readonly By _tableDataElementsFromColoumn2To5 = By.XPath("//table[@class='ui single line sortable striped table sortableHeader']//tr//td[position()=2 or position()=5]");
        private readonly By _acceptButtonElement = By.XPath("//button[normalize-space()='Accept']");
        private readonly By _declineButtonElement = By.XPath("//button[normalize-space()='Decline']");

        //Action methods
        public void OpenReceiveRequests()
        {
            var element = _state.Driver.FindElement(_manageRequestsTabElement);    //I got no such element exception
            Actions actions = new Actions(_state.Driver);
            actions.MoveToElement(element).Perform();
            _state.Driver.FindElement(_receivedRequestsElement).Click();
        }

        public void ClickAccept()
        {
            _state.Driver.FindElement(_acceptButtonElement).Click();
        }

        public void ClickDecline()
        {
            _state.Driver.FindElement(_declineButtonElement).Click();
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
    }
}

