using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages.ManageListingsComponent
{
    public class ManageListingsSendRequestComponent
    {
        private readonly TestState _state;
        public ManageListingsSendRequestComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _viewIconElement = By.XPath("//i[@class='eye icon']");
        private readonly By _requestTextAreaElement = By.XPath("//textarea[@placeholder='I am interested in trading my cooking skills with your coding skills..']");
        private readonly By _sendRequestIconElement = By.XPath("//i[@class='send outline icon']");
        private readonly By _notificationDropDownElement = By.XPath("//div[@class='ui top left pointing dropdown item']");
        private readonly By _seeAllElement = By.XPath("//a[normalize-space()='See All...']");
        private readonly By _goToPageElements = By.XPath("//div[@class='ui items segment']//div[@class='ui link']//a");
        private readonly By _notificationTitle = By.XPath("//*[normalize-space()='Service Request']");
        private readonly By _notificationMessage = By.XPath("//*[contains(normalize-space(),'Pending trade request')]");

        //Methods
        public void ClickViewIcon()
        {
            var viewIcon = _state.Driver.FindElement(_viewIconElement);
            viewIcon.Click();
        }

        public void EnterText(string requestMessage)
        {
            var requestTextArea = _state.Driver.FindElement(_requestTextAreaElement);
            requestTextArea.SendKeys(requestMessage);
        }

        public void ClickSendIcon()
        {
            var sendRequestIcon = _state.Driver.FindElement(_sendRequestIconElement);
            sendRequestIcon.Click();
        }

        public void SendRequest(string requestMessage)
        {
            EnterText(requestMessage);
            ClickSendIcon();
        }

        public void ClickNotificationLink()
        {
            _state.Driver.FindElement(_notificationDropDownElement).Click();
        }

        public void ClickSeeAll()
        {
            _state.Driver.FindElement(_seeAllElement).Click();
        }

        public IList<IWebElement> GetRequestLinks()
        {
            var listOfElements = _state.Driver.FindElements(_goToPageElements);
            return listOfElements;
        }

        public string GetTextRequestLinks()
        {
            var listOfElements = _state.Driver.FindElements(_goToPageElements);
            foreach (var element in listOfElements)
            {
                return element.Text;
            }
            return string.Empty;
        }

        public string GetPendingTradeRequestMessage()
        {
            var msg = _state.Driver.FindElement(_notificationMessage).Text;
            return msg.Trim();
        }

        public bool IsTradeRequestPresent(string expectedUser)
        {
            var requests = GetRequestLinks();
            return requests.Any(r => r.Text.Contains(expectedUser));
        }

        public bool IsRequestButtonEnabled()
        {
            var requestButton = _state.Driver.FindElement(_sendRequestIconElement).Enabled;
            return requestButton;
        }
    }
}
