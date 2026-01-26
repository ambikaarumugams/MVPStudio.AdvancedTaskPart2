using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Pages.Profile_Components;
using Reqnroll;
using NUnit.Framework;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "EducationDelete")]
    public class EducationDeleteSteps
    {
        private readonly TestState _state;
        private readonly EducationDeleteComponent _educationDeleteComponent;

        public EducationDeleteSteps(TestState state, EducationDeleteComponent educationDeleteComponent)
        {
            _state = state;
            _educationDeleteComponent = educationDeleteComponent;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _educationDeleteComponent.NavigateToTheProfilePage();
        }

        [When("I delete education details from json file with the TestName {string}")]  //Delete education details
        public void WhenIDeleteEducationDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var educationDetails = testItem.EducationDetailsToAdd; //Add
                _educationDeleteComponent.AddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var successMessage = _educationDeleteComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");

                var detailsToDelete = testItem.EducationDetailsToDelete;  //Delete 
                _educationDeleteComponent.DeleteSpecificEducation(detailsToDelete.CollegeUniversityName);
                var message = _educationDeleteComponent.GetSuccessMessage();
                _state.ActualEducationMessages.Add(message);
                if (detailsToDelete.EducationExpectedMessage != null)
                    _state.ExpectedEducationMessages.Add(detailsToDelete.EducationExpectedMessage);
            }
        }

        [Then("I should see the success message for delete")] //Validation for delete
        public void ThenIShouldSeeTheSuccessMessageForDelete()
        {
            _state.Assert.ListsMatch(_state.ActualEducationMessages, _state.ExpectedEducationMessages);
        }

        [When("I delete education details from json file after the session has expired with the TestName {string}")] //Delete the education details when session expired
        public void WhenIDeleteEducationDetailsFromJsonFileAfterTheSessionHasExpiredWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<EducationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var educationDetails = testItem.EducationDetailsToAdd;  //Add
                _educationDeleteComponent.AddEducationDetails(educationDetails.CollegeUniversityName, educationDetails.Country, educationDetails.Title, educationDetails.Degree, educationDetails.YearOfGraduation);
                var successMessage = _educationDeleteComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                _state.CleanupEducationAdd.Add(educationDetails.CollegeUniversityName);

                var detailsToDelete = testItem.EducationDetailsToDelete;  //Delete
                _educationDeleteComponent.ExpireSession();
                _educationDeleteComponent.DeleteSpecificEducation(detailsToDelete.CollegeUniversityName);
                var errorMessage = _educationDeleteComponent.GetErrorMessage();
                _state.ActualEducationMessages.Add(errorMessage);
                _state.ExpectedEducationMessages.Add(detailsToDelete.EducationExpectedMessage);
            }
        }

        [Then("I should login again to perform cleanup")]
        public void ThenIShouldLoginAgainToPerformCleanup()
        {
            _educationDeleteComponent.ClickSignOutButton();
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _educationDeleteComponent.NavigateToTheProfilePage();
        }

        [Then("I should see the error message to delete for session expired")] //Validation for session expired
        public void ThenIShouldSeeTheErrorMessageToDeleteForSessionExpired()
        {
            _state.Assert.ListsMatch(_state.ActualEducationMessages, _state.ExpectedEducationMessages);
        }
    }
}
