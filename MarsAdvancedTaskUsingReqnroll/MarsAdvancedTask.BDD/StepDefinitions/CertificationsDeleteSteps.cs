using Reqnroll;
using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Pages.Profile_Components;
using MarsAdvancedTask.Framework.Models;
using NUnit.Framework;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "CertificationsDelete")]
    public class CertificationsDeleteSteps
    {
        private readonly TestState _state;
        private readonly CertificationsDeleteComponent _certificationsDeleteComponent;

        public CertificationsDeleteSteps(TestState state, CertificationsDeleteComponent certificationsDeleteComponent)
        {
            _state = state;
            _certificationsDeleteComponent = certificationsDeleteComponent;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _certificationsDeleteComponent.NavigateToTheProfilePage();
        }

        [When("I delete certification details with the TestName {string}")]
        public void WhenIDeleteCertificationDetailsWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd; //Add
                _certificationsDeleteComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var successMessage = _certificationsDeleteComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");

                var detailsToDelete = testItem.CertificationDetailsToDelete;  //Delete 
                _certificationsDeleteComponent.DeleteSpecificCertification(detailsToDelete.CertificateOrAward);
                var message = _certificationsDeleteComponent.GetSuccessMessage();
                _state.ActualCertificationMessages.Add(message);
                if (detailsToDelete.CertificationExpectedMessage != null)
                    _state.ExpectedCertificationMessages.Add(detailsToDelete.CertificationExpectedMessage);
            }
        }

        [Then("I should see the success message for delete")]
        public void ThenIShouldSeeTheSuccessMessageForDelete()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }

        [When("I delete certification details from json file after the session has expired with the TestName {string}")]
        public void WhenIDeleteCertificationDetailsFromJsonFileAfterTheSessionHasExpiredWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;  //Add
                _certificationsDeleteComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var successMessage = _certificationsDeleteComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);

                var detailsToDelete = testItem.CertificationDetailsToDelete;  //Delete
                _certificationsDeleteComponent.ExpireSession();
                _certificationsDeleteComponent.DeleteSpecificCertification(detailsToDelete.CertificateOrAward);
                var errorMessage = _certificationsDeleteComponent.GetErrorMessage();
                _state.ActualCertificationMessages.Add(errorMessage);
                _state.ExpectedCertificationMessages.Add(detailsToDelete.CertificationExpectedMessage);
            }
        }

        [Then("I should login again to perform cleanup")]
        public void ThenIShouldLoginAgainToPerformCleanup()
        {
            _certificationsDeleteComponent.ClickSignOutButton();
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _certificationsDeleteComponent.NavigateToTheProfilePage();
        }

        [Then("I should see the error message to delete for session expired")]
        public void ThenIShouldSeeTheErrorMessageToDeleteForSessionExpired()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }
    }
}
