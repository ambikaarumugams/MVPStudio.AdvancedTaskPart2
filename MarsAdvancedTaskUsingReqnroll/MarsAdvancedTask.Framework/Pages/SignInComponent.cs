using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages
{
    public class SignInComponent
    {
        private readonly TestState _state;

        public SignInComponent(TestState state) //Inject Test State through constructor
        {
            _state = state;
        }

        //Locators
        private readonly By _signInButtonElement = By.XPath("//a[normalize-space()='Sign In']");
        private readonly By _userNameElement = By.XPath("//input[@placeholder='Email address']");
        private readonly By _passwordElement = By.XPath("//input[@placeholder='Password']");
        private readonly By _loginButtonElement = By.XPath("//button[normalize-space()='Login']");
        private readonly By _welcomeMessageElement = By.XPath("//span[contains(@class, 'dropdown') and contains(normalize-space(), 'Ambika')]");
        private readonly By _logoutButtonElement = By.XPath("//button[normalize-space()='Sign Out']");

        //Action Methods
        public void ClickSignInButton()
        {
            _state.Wait.SafeClick(_signInButtonElement);
        }

        public void EnterUserName(string username)
        {
            _state.Wait.SafeSendKeys(_userNameElement, username);
        }

        public void EnterPassword(string password)
        {
            _state.Wait.SafeSendKeys(_passwordElement, password);
        }

        public void ClickLoginButton()
        {
            _state.Wait.WaitUntilElementToBeClickable(_loginButtonElement).Click();
        }

        public string GetWelcomeMessage()
        {
            return _state.Wait.WaitUntilElementIsVisible(_welcomeMessageElement).Text;
        }

        public void SignIn(string username, string password)
        {
            ClickSignInButton();
            EnterUserName(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public void SignOut()
        {
            _state.Wait.WaitUntilElementToBeClickable(_logoutButtonElement).Click();
        }
    }
}

