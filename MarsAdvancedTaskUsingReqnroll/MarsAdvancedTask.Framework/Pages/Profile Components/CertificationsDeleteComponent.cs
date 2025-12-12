using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedTask.Framework.Pages.Profile_Components
{
    public class CertificationsDeleteComponent
    {
        private readonly TestState? _state;

        public CertificationsDeleteComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        //Add
        private readonly By _profileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By _certificationsTab = By.XPath("//a[normalize-space()='Certifications']");
        private readonly By _addNewButton = By.XPath("//div[@class='ui bottom attached tab segment tooltip-target active']//div[contains(@class,'ui teal button')][normalize-space()='Add New']");
        private readonly By _certificationsTable = By.XPath("//div[@data-tab='fourth']//table[@class='ui fixed table']");
        private readonly By _certificateOrAwardField = By.XPath("//input[@placeholder='Certificate or Award']");
        private readonly By _certificateFromField = By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']");
        private readonly By _certificationYearDropDown = By.XPath("//select[@name='certificationYear']");
        private readonly By _addButton = By.XPath("//input[@value='Add']");
        private readonly By _signOut = By.XPath("//button[normalize-space()='Sign Out']");

        //Action Methods
        public void NavigateToTheProfilePage()
        {
            var profileElement = _state.Wait.WaitUntilElementToBeClickable(_profileTab);
            profileElement.Click();

            var certificationElement = _state.Wait.WaitUntilElementToBeClickable(_certificationsTab);
            certificationElement.Click();
        }

        public void ClickAddNewButton()  //Click "Add New" button
        {
            var addNewButtonElement = _state.Wait.WaitUntilElementToBeClickable(_addNewButton);
            addNewButtonElement.Click();
        }

        public void AddCertifications(string certificationOrAward, string certificationFrom, string certificationYear) //Add certification details 
        {
            ClickAddNewButton();
            var certificationOrAwardElement = _state.Wait.WaitUntilElementIsVisible(_certificateOrAwardField);
            certificationOrAwardElement.SendKeys(certificationOrAward);

            var certificateFromElement = _state.Wait.WaitUntilElementIsVisible(_certificateFromField);
            certificateFromElement.SendKeys(certificationFrom);

            var certificationYearElement = _state.Wait.WaitUntilElementToBeClickable(_certificationYearDropDown);
            SelectElement selectCertificationYear = new SelectElement(certificationYearElement); //Select the "Year" from the drop-down
            selectCertificationYear.SelectByText(certificationYear);
            ClickAddButton();
        }

        public void ClickAddButton()   //Click "Add" button
        {
            var addButtonElement = _state.Wait.WaitUntilElementIsVisible(_addButton);
            addButtonElement.Click();
        }

        public string GetSuccessMessage() //Get the success message 
        {
            try
            {
                Thread.Sleep(3000);
                var successMessage = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
                return successMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetErrorMessage() //Get the error message
        {
            var errorMessage = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']"));
            return errorMessage.Text;
        }

        public void ClickSignOutButton()
        {
            _state.Wait.WaitUntilElementToBeClickable(_signOut).Click();
        }

        public void DeleteSpecificCertification(string certificationOrAwardToBeDeleted)  //Delete the specific certification details
        {
            var certificationTable = _state.Wait.WaitUntilElementIsVisible(_certificationsTable);  //Table Element
            var row = certificationTable.FindElement(By.XPath($".//tr[td[1]='{certificationOrAwardToBeDeleted}']")); //From the table find the row element to be deleted by passing the value of the certificate
            var deleteIcon = row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]")); //find the delete icon of the corresponding certificate value
            deleteIcon.Click();
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
