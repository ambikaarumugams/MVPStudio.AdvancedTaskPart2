using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V140.DOM;

namespace MarsAdvancedTask.Framework.Pages.ManageListingsComponent
{
    public class ManageListingsViewComponent
    {
        private readonly TestState _state;

        public ManageListingsViewComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _manageListingsTabElement = By.XPath("//a[normalize-space()='Manage Listings']");
        private readonly By _manageListingsHeadingElement = By.XPath("//h2[normalize-space()='Manage Listings']");
        private readonly By _manageListingsTableWithPageNumber = By.XPath("//body/div/div/div[@id='listing-management-section']/div[2]/div[1]");
        private readonly By _manageListingsTable = By.XPath("//table[@class='ui striped table']");
        private readonly By _viewIconElement = By.XPath("//i[@class='eye icon']");
        private readonly By _chatElement = By.XPath("//i[@class='comment icon']");
        private readonly By _chatTextBoxElement = By.XPath("//input[@id='chatTextBox']");
        private readonly By _requestButtonElement = By.XPath("//div[@class='ui teal disabled button']");
        private readonly By _titleElement = By.XPath("//span[@class='skill-title']");
        private readonly By _viewManageListingsElement = By.XPath("//div[@class='ten wide column']//div[@class='ui fluid card']");
        private readonly By _eyeIconElements = By.XPath("//div[@class='ui small icon buttons basic vertical']/child::button//i[@class='eye icon']");
        private readonly By _sendButtonElement = By.XPath("//button[@id='btnSend']");
        private readonly By _chatRoomContainerElement = By.XPath("//div[@id='chatRoomContainer']");

        public void ClickManageListingsTab()
        {
            var manageListingsTab = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTabElement);
            manageListingsTab.Click();
        }

        public void ViewManageListings()
        {
            var manageListingsHeading = _state.Wait.WaitUntilElementIsVisible(_manageListingsHeadingElement);
            var viewIcon = manageListingsHeading.FindElement(_viewIconElement);
            viewIcon.Click();
        }

        public bool IsChatEnabled()
        {
            try
            {
                var chat = _state.Wait.WaitUntilElementToBeClickable(_chatElement);
                return chat.Enabled && chat.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public bool IsRequestEnabled()
        {
            try
            {
                var request = _state.Wait.WaitUntilElementToBeClickable(_requestButtonElement);
                return request.Enabled && request.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public string GetWholeTableContent()
        {
            var manageListingsTableWithPageNumber = _state.Wait.WaitUntilElementIsVisible(_manageListingsTableWithPageNumber);
            return manageListingsTableWithPageNumber.Text;
        }

        public string GetTextFromViewManageListings()
        {
            var viewManageListings = _state.Wait.WaitUntilElementIsVisible(_viewManageListingsElement);
            return viewManageListings.Text;
        }

        public string GetTitle()
        {
            var title = _state.Wait.WaitUntilElementIsVisible(_titleElement);
            return title.Text;
        }

        public List<string> ClickAllEyeIcons()
        {
            var titles = new List<string>();
            _state.Wait.WaitUntilElementIsVisible(_manageListingsHeadingElement);
            var eyeIcons = _state.Driver.FindElements(_eyeIconElements);
            foreach (var eyeIcon in eyeIcons)
            {
                eyeIcon.Click();
                titles.Add(GetTitle());

                _state.Driver.Navigate().Back();
                _state.Wait.WaitUntilElementIsVisible(_manageListingsHeadingElement);
            }
            return titles;
        }

        public List<List<string>> GetManageListingsTable()
        {
            var manageListingsTable = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTable);
            var rows = manageListingsTable.FindElements(By.XPath("//tbody//tr"));
            var allData = new List<List<string>>();

            foreach (var row in rows)
            {
                var cells = row.FindElements(By.XPath("./td[position() >= 2 and position() <= 4]"));
                var cellValues = cells.Select(c => c.Text.Trim()).ToList();
                allData.Add(cellValues);
            }
            return allData;
        }

        public void ClickChat()
        {
            var chat = _state.Wait.WaitUntilElementToBeClickable(_chatElement);
            chat.Click();
        }

        public void EnterMessageInChatBox(string text)
        {
            var chatTextBox = _state.Wait.WaitUntilElementToBeClickable(_chatTextBoxElement);
            chatTextBox.Clear();
            chatTextBox.SendKeys(text);
        }

        public void ClickSendButton()
        {
            var sendButton = _state.Wait.WaitUntilElementToBeClickable(_sendButtonElement);
            sendButton.Click();
        }

        public string GetChatMessage()
        {
            var chatMessageHistory = _state.Wait.WaitUntilElementIsVisible(_chatRoomContainerElement);
            string chatRoomText = chatMessageHistory.Text.Trim();
            return chatRoomText;
        }
    }
}
