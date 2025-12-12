using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedTask.Framework.Pages.Profile_Components
{
    public class EducationAddComponent
    {
        private readonly TestState _state;

        //Constructor
        public EducationAddComponent(TestState state) //Inject the state
        {
            _state = state;
        }

        //Locators
        //Add
        private readonly By _profileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By _educationTab = By.XPath("//form[@class='ui form']//a[normalize-space()='Education']");
        private readonly By _addNewButton = By.XPath("//div[@class='ui bottom attached tab segment tooltip-target active']//div[contains(@class,'ui teal button')][normalize-space()='Add New']");
        private readonly By _educationTable = By.XPath("//div[@data-tab='third']//table[@class='ui fixed table']");
        private readonly By _collegeUniversityNameField = By.XPath("//input[@placeholder='College/University Name']");
        private readonly By _countryDropDown = By.XPath("//select[@name='country']");
        private readonly By _titleDropDown = By.XPath("//select[@name='title']");
        private readonly By _degreeField = By.XPath("//input[@placeholder='Degree']");
        private readonly By _yearOfGraduationDropDown = By.XPath("//select[@name='yearOfGraduation']");
        private readonly By _addButton = By.XPath("//input[@value='Add']");
        private readonly By _cancelButton = By.XPath("//input[@value='Cancel']");
        private readonly By _successMessage = By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']");
        private readonly By _errorMessage = By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']");

        //Action Methods
        public void NavigateToTheProfilePage()  //Navigate to the education page 
        {
            var profileElement = _state.Wait.WaitUntilElementToBeClickable(_profileTab);
            profileElement.Click();

            var educationElement = _state.Wait.WaitUntilElementToBeClickable(_educationTab);
            educationElement.Click();
        }

        public void ClickAddNewButton()
        {
            //Click "Add New" button
            var addNewElement = _state.Wait.WaitUntilElementToBeClickable(_addNewButton);
            addNewElement.Click();
        }

        public void AddEducationDetails(string universityName, string countryName, string title, string degree, string year) //Add education details
        {
            ClickAddNewButton();
            //Enter College/University Name
            var enterCollegeUniversityName = _state.Wait.WaitUntilElementToBeClickable(_collegeUniversityNameField);
            enterCollegeUniversityName.SendKeys(universityName);

            //Select the country name using drop down
            var selectCountryDropDown = _state.Wait.WaitUntilElementToBeClickable(_countryDropDown);

            SelectElement selectCountry = new SelectElement(selectCountryDropDown);
            selectCountry.SelectByText(countryName);

            //Select the title using drop down
            var titleDropDown = _state.Wait.WaitUntilElementToBeClickable(_titleDropDown);

            SelectElement selectTitle = new SelectElement(titleDropDown);
            selectTitle.SelectByText(title);

            //Enter the degree
            var degreeElement = _state.Wait.WaitUntilElementToBeClickable(_degreeField);
            degreeElement.SendKeys(degree);

            //Select the year of graduation drop down
            var selectYearOfGraduationDropDown = _state.Wait.WaitUntilElementToBeClickable(_yearOfGraduationDropDown);

            SelectElement selectYear = new SelectElement(selectYearOfGraduationDropDown);
            selectYear.SelectByText(year);
            ClickAddButton();
        }

        public void ClickAddButton()
        {
            //Click "Add" button
            var addButton = _state.Wait.WaitUntilElementToBeClickable(_addButton);
            addButton.Click();
        }

        public string GetSuccessMessage() //Get success Message
        {
            try
            {
                Thread.Sleep(3000);
                var successMessage = _state.Wait.WaitUntilElementIsVisible(_successMessage);
                return successMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void DeleteSpecificEducation(string educationToBeDeleted)  //Delete specific education
        {
            var educationTable = _state.Wait.WaitUntilElementToBeClickable(_educationTable);
            var row = educationTable.FindElement(By.XPath($".//tr[td[2]='{educationToBeDeleted}']")); //University name is in the second column
            var deleteIcon = row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
            deleteIcon.Click();
        }

        public string GetErrorMessage()  //Get error message
        {
            try
            {
                var errorMessage = _state.Wait.WaitUntilElementIsVisible(_errorMessage);
                return errorMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public (string MessageText, string MessageType) GetToastMessage()  //Tuples to get both success and error 
        {
            try
            {
                var Wait = new WebDriverWait(_state.Driver, TimeSpan.FromSeconds(3));

                var toastMessageElement = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'ns-type-') and contains(@class,'ns-show')]")));
                Thread.Sleep(3000);
                var messageText = toastMessageElement.Text.Trim();
                var classAttribute = string.Empty;
                var messageType = string.Empty;

                classAttribute = toastMessageElement.GetAttribute("class");
                if (classAttribute != null)
                {
                    messageType = classAttribute.Contains("ns-type-success") ? "success" :
                                  classAttribute.Contains("ns-type-error") ? "error" : "none";
                }
                return (messageText, messageType);
            }
            catch
            {
                return ("", "error");
            }
        }

        public void LeaveEitherOneOrAllTheFieldsEmptyToAdd(string universityName, string countryName, string title, string degree, string year)  //Leave either one or all the fields empty
        {
            ClickAddNewButton();

            //Enter College/University Name
            var enterCollegeUniversityName = _state.Wait.WaitUntilElementToBeClickable(_collegeUniversityNameField);
            if (!string.IsNullOrWhiteSpace(universityName))
            {
                enterCollegeUniversityName.SendKeys(universityName);
            }
            //Select the country name using drop down
            var selectCountryDropDown = _state.Wait.WaitUntilElementToBeClickable(_countryDropDown);

            SelectElement selectCountry = new SelectElement(selectCountryDropDown);
            if (!string.IsNullOrWhiteSpace(countryName))
            {
                selectCountry.SelectByText(countryName);
            }
            else
            {
                selectCountry.SelectByIndex(0);
            }
            //Select the title using drop down
            var titleDropDown = _state.Wait.WaitUntilElementToBeClickable(_titleDropDown);

            SelectElement selectTitle = new SelectElement(titleDropDown);
            if (!string.IsNullOrWhiteSpace(title))
            {
                selectTitle.SelectByText(title);
            }
            else
            {
                selectTitle.SelectByIndex(0);
            }
            //Enter the degree
            var degreeElement = _state.Wait.WaitUntilElementToBeClickable(_degreeField);
            if (!string.IsNullOrWhiteSpace(degree))
            {
                degreeElement.SendKeys(degree);
            }
            //Select the year of graduation drop down
            var selectYearOfGraduationDropDown = _state.Wait.WaitUntilElementToBeClickable(_yearOfGraduationDropDown);

            SelectElement selectYear = new SelectElement(selectYearOfGraduationDropDown);
            if (!string.IsNullOrWhiteSpace(year))
            {
                selectYear.SelectByText(year);
            }
            else
            {
                selectYear.SelectByIndex(0);
            }
            ClickAddButton();
            ClickCancelButton();
        }

        public void ClickCancelButton()  //Click cancel button
        {
            var cancelElement = _state.Wait.WaitUntilElementToBeClickable(_cancelButton);
            cancelElement.Click();

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

        public bool IsEducationEmpty(string universityName)
        {
            try
            {
                var educationTable = _state.Wait.WaitUntilElementToBeClickable(_educationTable);
                var row = educationTable.FindElement(
                    By.XPath($".//tr[td[normalize-space(text())='{universityName}']]"));
                return false;
            }
            catch
            {
                return true;
            }
        }

        public void CancelAddEducationDetails(string universityName, string countryName, string title, string degree, string year) //Cancel add education details
        {
            ClickAddNewButton();
            //Enter College/University Name
            var enterCollegeUniversityName = _state.Wait.WaitUntilElementToBeClickable(_collegeUniversityNameField);
            enterCollegeUniversityName.SendKeys(universityName);

            //Select the country name using drop down
            var selectCountryDropDown = _state.Wait.WaitUntilElementToBeClickable(_countryDropDown);

            SelectElement selectCountry = new SelectElement(selectCountryDropDown);
            selectCountry.SelectByText(countryName);

            //Select the title using drop down
            var titleDropDown = _state.Wait.WaitUntilElementToBeClickable(_titleDropDown);

            SelectElement selectTitle = new SelectElement(titleDropDown);
            selectTitle.SelectByText(title);

            //Enter the degree
            var degreeElement = _state.Wait.WaitUntilElementToBeClickable(_degreeField);
            degreeElement.SendKeys(degree);

            //Select the year of graduation drop down
            var selectYearOfGraduationDropDown = _state.Wait.WaitUntilElementToBeClickable(_yearOfGraduationDropDown);

            SelectElement selectYear = new SelectElement(selectYearOfGraduationDropDown);
            selectYear.SelectByText(year);
            ClickCancelButton();
        }
    }
}




