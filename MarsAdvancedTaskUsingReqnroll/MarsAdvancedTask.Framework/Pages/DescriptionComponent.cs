using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace MarsAdvancedTask.Framework.Pages
{
    public class DescriptionComponent
    {
        private readonly TestState _state;
        public DescriptionComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _profileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By _descriptionHeaderElement = By.XPath("//h3[normalize-space()='Description']");
        private readonly By _editIconElement = By.XPath("//i[@class='outline write icon']");
        private readonly By _textAreaElement = By.XPath("//textarea[@placeholder='Please tell us about any hobbies, additional expertise, or anything else you’d like to add.']");
        private readonly By _saveButtonElement = By.XPath("//button[@type='button']");
        private readonly By _characterRemainingElement = By.XPath("//p[contains(text(), 'Characters remaining')]");
        private readonly By _successMessageElement = By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']");
        private readonly By _errorMessageElement = By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']");
     
        //Action Methods
        public void NavigateToTheProfilePage()
        {
            var profileElement = _state.Wait.WaitUntilElementToBeClickable(_profileTab);
            profileElement.Click();
        }

        public void ClickEditIcon()
        {
            var descriptionHeader = _state.Wait.WaitUntilElementToBeClickable(_descriptionHeaderElement);
            var editIcon = descriptionHeader.FindElement(_editIconElement);
            editIcon.Click();
        }

        public void EnterTextInTheDescriptionBox(string text)
        {
            var textArea = _state.Wait.WaitUntilElementToBeClickable(_textAreaElement);
            textArea.Clear();
            textArea.SendKeys(text);
            ClickSaveButton();
        }

        public void ClickSaveButton()
        {
            var saveButton = _state.Wait.WaitUntilElementToBeClickable(_saveButtonElement);
            saveButton.Click();
        }

        public string GetTextAreaPlaceholderText()
        {
            var textAreaPlaceholder = _state.Wait.WaitUntilElementIsVisible(_textAreaElement);
            var placeholderText = textAreaPlaceholder.Text;
            return placeholderText;
        }

        public string GetCharactersRemainingText()
        {
            var charactersRemaining = _state.Wait.WaitUntilElementIsVisible(_characterRemainingElement);
            var text = charactersRemaining.Text;
            return text;
        }

        public string GetSuccessMessage()
        {
            var successMessage = _state.Wait.WaitUntilElementIsVisible(_successMessageElement).Text;
            return successMessage;
        }

        public string GetErrorMessage()
        {
            var errorMessage = _state.Wait.WaitUntilElementIsVisible(_errorMessageElement).Text;
            return errorMessage;
        }

        public (string MessageText, string MessageType) GetToastMessage() //Get both error and success message using tuples
        {
            var wait = new WebDriverWait(_state.Driver, TimeSpan.FromSeconds(3));
            var toastMessageElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'ns-type-') and contains(@class,'ns-show')]"))); //Without value capture the element
            Thread.Sleep(3000);
            var messageText = toastMessageElement.Text.Trim(); //Get the text
            var classAttribute = string.Empty;
            var messageType = string.Empty;

            classAttribute = toastMessageElement.GetAttribute("class"); //Get the class attribute
            if (classAttribute.Contains("ns-type-success"))
            {
                messageType = "success";
            }
            else if (classAttribute.Contains("ns-type-error"))
            {
                messageType = "error";
            }
            else
            {
                messageType = "none";
            }
            return (messageText, messageType);
        }

        public void ClearText()
        {
            var textArea = _state.Wait.WaitUntilElementToBeClickable(_textAreaElement);
            textArea.Clear();
        }

        public string GetDescriptionText()
        {
            Thread.Sleep(3000);
            var textArea = _state.Driver.FindElement(By.XPath("//span[@style='padding-top: 1em;']"));
            return textArea.Text;
        }

        public string GetTextAfterEnterTheDescription()
        {
            var textValue = _state.Driver.FindElement(By.XPath("//textarea[@name='value']"));
            return textValue.GetAttribute("value"); //To get the text that we've passed
        }

        public void LeaveTheDescriptionFieldEmpty(string text)
        {
            var textArea = _state.Wait.WaitUntilElementToBeClickable(_textAreaElement);
            textArea.Clear();
            Thread.Sleep(3000);
            textArea.SendKeys(Keys.Control + "a" + Keys.Delete);
            if (!string.IsNullOrWhiteSpace(text))
            {
                textArea.SendKeys(text);
            }
            ClickSaveButton();
        }
    }
}




