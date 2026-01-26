using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.Profile_Components;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "EducationAdd")]
    public class EducationAddSteps
    {
        private readonly TestState _state;
        private readonly EducationAddComponent _educationAddComponent;

        public EducationAddSteps(TestState state, EducationAddComponent educationAddComponent)
        {
            _state = state;
            _educationAddComponent = educationAddComponent;
        }

        [Given("I navigate to the profile page as a registered user")] //Login and navigate to the profile page
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _educationAddComponent.NavigateToTheProfilePage();
        }

        [When("I enter education details from json file with the TestName {string}")]  //Add education details
        public void WhenIEnterEducationDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var educationDetails = testItem.EducationDetailsToAdd;
                _educationAddComponent.AddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var successMessage = _educationAddComponent.GetSuccessMessage();
                _state.ActualEducationMessages.Add(successMessage);

                if (educationDetails.EducationExpectedMessage != null)
                    _state.ExpectedEducationMessages.Add(educationDetails.EducationExpectedMessage);
                _state.CleanupEducationAdd.Add(educationDetails.CollegeUniversityName);
            }
        }

        [Then("I should see the success message")]  //Validation for add education
        public void ThenIShouldSeeTheSuccessMessage()
        {
            _state.Assert.ListsMatch(_state.ActualEducationMessages, _state.ExpectedEducationMessages);
        }

        [When("I enter invalid education details from json file with the TestName {string}")] //Add invalid education details (college/university name and degree)
        public void WhenIEnterInvalidEducationDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var educationDetails = testItem.EducationDetailsToAdd;
                _educationAddComponent.AddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var (messageText, messageType) = _educationAddComponent.GetToastMessage();
                Console.WriteLine($"Message:{messageText},{messageType}");
                Thread.Sleep(3000);
                _state.EducationTuplesMessages.Add((messageText, messageType));

                if (string.Equals(messageType, "Success", StringComparison.OrdinalIgnoreCase))
                {
                    _state.CleanupEducationAdd.Add(educationDetails.CollegeUniversityName);
                }
                _state.ExpectedEducationMessages.Add(educationDetails.EducationExpectedMessage);
            }
        }

        [Then("I should see the error message")] //Validation for invalid education details
        public void ThenIShouldSeeTheErrorMessage()
        {
            foreach (var expectedEducationMessage in _state.ExpectedEducationMessages)
            {
                _state.Assert.AssertToastMessageForInvalid(_state.EducationTuplesMessages, expectedEducationMessage);
            }
        }

        [Then("I should see the error message for adding huge string")] //Validation for adding huge string
        public void ThenIShouldSeeTheErrorMessageForAddingHugeString()
        {
            foreach (var expectedMessage in _state.ExpectedEducationMessages)
            {
                _state.Assert.ListContainsString(_state.ActualEducationMessages, expectedMessage);
            }
        }

        [When("I leave either one or all the fields empty and give the data from json file with the TestName {string}")] //Leave either one or all the fields are empty
        public void WhenILeaveEitherOneOrAllTheFieldsEmptyAndGiveTheDataFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var educationDetails = testItem.EducationDetailsToAdd;
                _educationAddComponent.LeaveEitherOneOrAllTheFieldsEmptyToAdd(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var errorMessage = _educationAddComponent.GetErrorMessage();
                _state.ActualEducationMessages.Add(errorMessage);

                if (educationDetails.EducationExpectedMessage != null)
                    _state.ExpectedEducationMessages.Add(educationDetails.EducationExpectedMessage);
            }
        }

        [Then("I should see the error message for empty fields")]  //Validation for empty fields
        public void ThenIShouldSeeTheErrorMessageForEmptyFields()
        {
            _state.Assert.AssertListContainsAll(_state.ActualEducationMessages, _state.ExpectedEducationMessages);
        }

        [When("I enter same education details twice from json file with the TestName {string}")]  //Adding same details multiple times
        public void WhenIEnterSameEducationDetailsTwiceFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var educationDetails = testItem.EducationDetailsToAdd;
                _educationAddComponent.AddEducationDetails(educationDetails.CollegeUniversityName,
                    educationDetails.Country, educationDetails.Title, educationDetails.Degree,
                    educationDetails.YearOfGraduation);
                var (messageText, messageType) = _educationAddComponent.GetToastMessage();
                Console.WriteLine($"Message:{messageText},{messageType}");
                Thread.Sleep(3000);

                if (string.Equals(messageType, "Success", StringComparison.OrdinalIgnoreCase))
                {
                    _state.CleanupEducationAdd.Add(educationDetails.CollegeUniversityName);
                    _state.ActualEducationMessages.Add(messageText);
                }
                else if (string.Equals(messageType, "Error", StringComparison.OrdinalIgnoreCase))
                {
                    _state.ActualEducationMessages.Add(messageText);
                }
                _state.ExpectedEducationMessages.Add(educationDetails.EducationExpectedMessage);
            }
        }

        [Then("I should see the error message for duplicate data")]  //Validation for adding duplicate data
        public void ThenIShouldSeeTheErrorMessageForDuplicateData()
        {
            _state.Assert.ListsMatch(_state.ActualEducationMessages, _state.ExpectedEducationMessages);
        }

        [When("I enter education details from json file after the session has expired with the TestName {string}")]  //Add education details when session expired
        public void WhenIEnterEducationDetailsFromJsonFileAfterTheSessionHasExpiredWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItems in scenario.TestItems)
            {
                var educationDetails = testItems.EducationDetailsToAdd;
                _educationAddComponent.ExpireSession();
                _educationAddComponent.AddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var errorMessage = _educationAddComponent.GetErrorMessage();
                _state.ActualEducationMessages.Add(errorMessage);
                _state.ExpectedEducationMessages.Add(educationDetails.EducationExpectedMessage);
            }
        }

        [Then("I should see the error message for session expired")]  //Validation for session expired
        public void ThenIShouldSeeTheErrorMessageForSessionExpired()
        {
            _state.Assert.ListsMatch(_state.ActualEducationMessages, _state.ExpectedEducationMessages);
        }

        [Then("I should see the error message for adding education details")]  //Validation for negative testing with valid data
        public void ThenIShouldSeeTheErrorMessageForAddingEducationDetails()
        {
            foreach (var expectedEducationMessage in _state.ExpectedEducationMessages)
            {
                _state.Assert.ListContainsString(_state.ActualEducationMessages, expectedEducationMessage);
            }
        }

        [When("I enter education details from the Json file with the test name {string}")]  //Cancel add education details
        public void WhenIEnterEducationDetailsFromTheJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItems in scenario.TestItems)
            {
                var educationDetails = testItems.EducationDetailsToAdd;
                _educationAddComponent.CancelAddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var actual = _educationAddComponent.IsEducationEmpty(educationDetails.CollegeUniversityName);
                _state.ActualEducationFlags.Add(actual);
            }
        }

        [Then("I should see the education details shouldn't be added")]  //Validation for cancel add
        public void ThenIShouldSeeTheEducationDetailsShouldntBeAdded()
        {
            _state.Assert.IsTrue(_state.ActualEducationFlags);
        }

        [When("I enter education details for destructive testing from json file with the TestName {string}")]  //Add education details for destructive testing
        public void WhenIEnterEducationDetailsForDestructiveTestingFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItems in scenario.TestItems)
            {
                var educationDetails = testItems.EducationDetailsToAdd;

                if (educationDetails.CollegeUniversityName.Equals("CollegeName_Text_5000"))
                {
                    educationDetails.CollegeUniversityName = new string('A', 5000);
                }
                _educationAddComponent.AddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var successMessage = _educationAddComponent.GetSuccessMessage();
                _state.ActualEducationMessages.Add(successMessage);
                _state.ExpectedEducationMessages.Add(educationDetails.EducationExpectedMessage);
                _state.CleanupEducationAdd.Add(educationDetails.CollegeUniversityName);
            }
        }

        [Then("I should see the error message for huge data")]  //Validation for destructive testing
        public void ThenIShouldSeeTheErrorMessageForHugeData()
        {
            foreach (var expectedMessage in _state.ExpectedEducationMessages)
            {
                _state.Assert.ListContainsString(_state.ActualEducationMessages, expectedMessage);
            }
        }
    }
}

