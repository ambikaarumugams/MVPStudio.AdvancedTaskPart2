using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using MarsAdvancedTask.Framework.Helpers;

namespace MarsAdvancedTask.Framework.Pages.Profile_Components
{
    public class CertificationsAddComponent
    {
        private readonly TestState _state;

        public CertificationsAddComponent(TestState state)
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
        private readonly By _cancelButton = By.XPath("//input[@value='Cancel']");

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
            var certificationOrAwardElement = _state.Wait.WaitUntilElementToBeClickable(_certificateOrAwardField);
            certificationOrAwardElement.SendKeys(certificationOrAward);

            var certificateFromElement = _state.Wait.WaitUntilElementToBeClickable(_certificateFromField);
            certificateFromElement.SendKeys(certificationFrom);

            var certificationYearElement = _state.Wait.WaitUntilElementToBeClickable(_certificationYearDropDown);
            SelectElement selectCertificationYear = new SelectElement(certificationYearElement); //Select the "Year" from the drop-down
            selectCertificationYear.SelectByText(certificationYear);
            ClickAddButton();
        }

        public void ClickAddButton()   //Click "Add" button
        {
            var addButtonElement = _state.Wait.WaitUntilElementToBeClickable(_addButton);
            addButtonElement.Click();
        }

        public string GetSuccessMessage() //Get the success message 
        {
            try
            {
                Thread.Sleep(3000);
                var successMessageElement = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
                return successMessageElement.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void DeleteSpecificCertification(string certificationOrAwardToBeDeleted)  //Delete the specific certification details
        {
            var certificationTable = _state.Wait.WaitUntilElementToBeClickable(_certificationsTable);  //Table Element
            var row = certificationTable.FindElement(By.XPath($".//tr[td[1]='{certificationOrAwardToBeDeleted}']")); //From the table find the row element to be deleted by passing the value of the certificate
            var deleteIcon = row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]")); //find the delete icon of the corresponding certificate value

            deleteIcon.Click();
        }

        public void LeaveEitherOneOrAllTheFieldsEmptyForAdd(string certificationOrAward, string certificationFrom, string certificationYear) //Leave either one or all the fields are empty for add certification details
        {
            ClickAddNewButton();
            var certificationOrAwardElement = _state.Wait.WaitUntilElementToBeClickable(_certificateOrAwardField);
            if (!string.IsNullOrWhiteSpace(certificationOrAward))  // If the string is not null or white space enter the value, otherwise skip
            {
                certificationOrAwardElement.SendKeys(certificationOrAward);
            }
            var certificateFromElement = _state.Wait.WaitUntilElementToBeClickable(_certificateFromField);
            if (!string.IsNullOrWhiteSpace(certificationFrom))
            {
                certificateFromElement.SendKeys(certificationFrom);
            }

            var certificationYearElement = _state.Wait.WaitUntilElementToBeClickable(_certificationYearDropDown);
            SelectElement selectCertificationYear = new SelectElement(certificationYearElement);
            if (!string.IsNullOrWhiteSpace(certificationYear)) //If the string is not null or white space select the year
            {
                selectCertificationYear.SelectByText(certificationYear);
            }
            else
            {
                selectCertificationYear.SelectByIndex(0);  //If it's null select the default index value
            }
            ClickAddButton();
        }

        public string GetErrorMessage() //Get the error message
        {
            var errorMessageElement = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']"));
            return errorMessageElement.Text;
        }

        public void ClickCancelButton() //Click the "Cancel" button
        {
            var cancelButtonElement = _state.Wait.WaitUntilElementToBeClickable(_cancelButton);
            cancelButtonElement.Click();
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

        public void CancelAddCertificationDetails(string certificationOrAward, string certificationFrom, string certificationYear) //Cancel the "Add" certification details
        {
            ClickAddNewButton();
            var certificationOrAwardElement = _state.Wait.WaitUntilElementToBeClickable(_certificateOrAwardField);
            certificationOrAwardElement.SendKeys(certificationOrAward);

            var certificateFromElement = _state.Wait.WaitUntilElementToBeClickable(_certificateFromField);
            certificateFromElement.SendKeys(certificationFrom);

            var certificationYearElement = _state.Wait.WaitUntilElementToBeClickable(_certificationYearDropDown);
            SelectElement selectCertificationYear = new SelectElement(certificationYearElement);
            selectCertificationYear.SelectByText(certificationYear);
            ClickCancelButton();
        }

        public bool IsCertificationEmpty(string certificateOrAward)
        {
            try
            {
                var certificationTable = _state.Wait.WaitUntilElementIsVisible(_certificationsTable);
                var row = certificationTable.FindElement(By.XPath($".//tr[td[normalize-space(text())='{certificateOrAward}']]"));
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
