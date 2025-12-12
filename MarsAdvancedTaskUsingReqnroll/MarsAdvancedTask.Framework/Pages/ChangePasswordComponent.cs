using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedTask.Framework.Pages
{
    public class ChangePasswordComponent
    {
        private readonly TestState _state;

        public ChangePasswordComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _welcomeMessageElement = By.XPath("//span[contains(@class, 'dropdown') and contains(normalize-space(), 'Ambika')]");
        private readonly By _changePasswordElement = By.XPath("//a[normalize-space()='Change Password']");
        private readonly By _currentPasswordElement = By.XPath("//input[@placeholder='Current Password']");
        private readonly By _newPasswordElement = By.XPath("//input[@placeholder='New Password']");
        private readonly By _confirmPasswordElement = By.XPath("//input[@placeholder='Confirm Password']");
        private readonly By _saveButtonElement = By.XPath("//button[@role='button']");

        private readonly By _successMessageElement = By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']");
        private readonly By _errorMessageElement = By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']");

        //Action Methods
        public void ClickWelcomeMessage()
        {
            _state.Wait.WaitUntilElementToBeClickable(_welcomeMessageElement).Click();
        }

        public void ClickChangePassword()
        {
            _state.Wait.WaitUntilElementIsVisible(_changePasswordElement).Click();
        }

        public void EnterCurrentPassword(string currentPassword)
        {
            var current = _state.Wait.WaitUntilElementIsVisible(_currentPasswordElement);
            current.Clear();
            current.SendKeys(currentPassword);
        }

        public void EnterNewPassword(string newPassword)
        {
            var newOne = _state.Wait.WaitUntilElementIsVisible(_newPasswordElement);
            newOne.Clear();
            newOne.SendKeys(newPassword);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            var confirm = _state.Wait.WaitUntilElementIsVisible(_confirmPasswordElement);
            confirm.Clear();
            confirm.SendKeys(confirmPassword);
        }

        public void ClickSaveButton()
        {
            _state.Wait.WaitUntilElementToBeClickable(_saveButtonElement).Click();
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

        public void ChangePassword(string currentPassword, string newPassword, string confirmPassword) //Change Password
        {
            ClickWelcomeMessage();
            ClickChangePassword();
            EnterCurrentPassword(currentPassword);
            EnterNewPassword(newPassword);
            EnterConfirmPassword(confirmPassword);
            ClickSaveButton();
        }

        public void LeaveEitherOneOrAllTheFieldsEmpty(string currentPassword, string newPassword, string confirmPassword)  //Leave either one or all the fields empty
        {
            ClickWelcomeMessage();
            ClickChangePassword();
            var current = _state.Wait.WaitUntilElementIsVisible(_currentPasswordElement);
            current.Clear();
            if (!string.IsNullOrWhiteSpace(currentPassword))
            {
                current.SendKeys(currentPassword);
            }
            var newOne = _state.Wait.WaitUntilElementIsVisible(_newPasswordElement);
            newOne.Clear();
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                newOne.SendKeys(newPassword);
            }
            var confirm = _state.Wait.WaitUntilElementIsVisible(_confirmPasswordElement);
            confirm.Clear();
            if (!string.IsNullOrWhiteSpace(confirmPassword))
            {
                confirm.SendKeys(confirmPassword);
            }
            ClickSaveButton();
        }

        public void ExpireSession() //To delete the token to get the session timeout message
        {
            try
            {
                _state.Driver.Manage().Cookies.DeleteCookieNamed("marsAuthToken");
            }
            catch
            {
            }
        }
    }
}

