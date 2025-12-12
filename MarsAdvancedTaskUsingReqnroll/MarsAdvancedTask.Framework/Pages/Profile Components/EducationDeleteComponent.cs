using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedTask.Framework.Pages.Profile_Components
{
    public class EducationDeleteComponent
    {
        private readonly TestState _state;

        public EducationDeleteComponent(TestState state)
        {
            _state = state;
        }

        //Locators
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
        private readonly By _signOut = By.XPath("//button[normalize-space()='Sign Out']");
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

        public void ClickSignOutButton()
        {
            _state.Wait.WaitUntilElementToBeClickable(_signOut).Click();
        }
    }
}
