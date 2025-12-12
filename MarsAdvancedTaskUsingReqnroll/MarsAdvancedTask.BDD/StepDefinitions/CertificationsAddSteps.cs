using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.Profile_Components;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "CertificationsAdd")]
    public class CertificationsAddSteps
    {
        private readonly TestState _state;
        private readonly CertificationsAddComponent _certificationsAddComponent;

        public CertificationsAddSteps(TestState state, CertificationsAddComponent certificationsAddComponent)
        {
            _state = state;
            _certificationsAddComponent = certificationsAddComponent;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _certificationsAddComponent.NavigateToTheProfilePage();
        }

        [When("I enter certification details from json file with the TestName {string}")]  //Add certifications with valid input
        public void WhenIEnterCertificationDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;
                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var successMessage = _certificationsAddComponent.GetSuccessMessage();
                _state.ActualCertificationMessages.Add(successMessage);

                if (certificationDetails.CertificationExpectedMessage != null)
                    _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
                _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);
            }
        }

        [Then("I should see the success message")] //Validation for add
        public void ThenIShouldSeeTheSuccessMessage()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }

        [When("I enter invalid certification details from json file with the TestName {string}")]  //Add invalid input (Certificate or Award and Certified from)
        public void WhenIEnterInvalidCertificationDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;
                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var successMessage = _certificationsAddComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                _state.ActualCertificationMessages.Add(successMessage);
                _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
                _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);
            }
        }

        [When("I enter lengthy Certificate or Award details from json file with the TestName {string}")]   //Add CertificateOrAward as a lengthy string 
        public void WhenIEnterLengthyCertificateOrAwardDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;
                if (certificationDetails.CertificateOrAward.Equals("{LONG_255}"))
                {
                    certificationDetails.CertificateOrAward = new string('s', 255);
                }

                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var actualMessage = _certificationsAddComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{actualMessage}");
                _state.ActualCertificationMessages.Add(actualMessage);
                _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
                _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);
            }
        }

        [When("I enter lengthy Certificate from details from json file with the TestName {string}")] //Add CertifiedFrom as a lengthy string 
        public void WhenIEnterLengthyCertificateFromDetailsFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;
                if (certificationDetails.CertifiedFrom.Equals("{LONG_300}"))
                {
                    certificationDetails.CertifiedFrom = new string('s', 300);
                }

                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var actualMessage = _certificationsAddComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{actualMessage}");
                _state.ActualCertificationMessages.Add(actualMessage);
                _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
                _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);
            }
        }

        [Then("I should see the error message")]  //Validation for invalid input
        public void ThenIShouldSeeTheErrorMessage()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }

        [When("I leave either one or all the fields empty and give the data from json file with the TestName {string}")]  //Leave either one or all the fields are empty
        public void WhenILeaveEitherOneOrAllTheFieldsEmptyAndGiveTheDataFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;
                _certificationsAddComponent.LeaveEitherOneOrAllTheFieldsEmptyForAdd(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var errorMessage = _certificationsAddComponent.GetErrorMessage();
                _state.ActualCertificationMessages.Add(errorMessage);
                _certificationsAddComponent.ClickCancelButton();
                if (certificationDetails.CertificationExpectedMessage != null)
                    _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
            }
        }

        [Then("I should see the error message for empty fields")] //Validation for empty fields
        public void ThenIShouldSeeTheErrorMessageForEmptyFields()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }

        [When("I enter same certification details twice from json file with the TestName {string}")]  //Add duplicate data
        public void WhenIEnterSameCertificationDetailsTwiceFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItem in scenario.TestItems)
            {
                var certificationDetails = testItem.CertificationDetailsToAdd;
                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var (messageText, messageType) = _certificationsAddComponent.GetToastMessage();
                Console.WriteLine($"Message:{messageText},{messageType}");
                Thread.Sleep(3000);

                if (string.Equals(messageType, "Success", StringComparison.OrdinalIgnoreCase))
                {
                    _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);
                    _state.ActualCertificationMessages.Add(messageText);
                }
                else if (string.Equals(messageType, "Error", StringComparison.OrdinalIgnoreCase))
                {
                    _state.ActualCertificationMessages.Add(messageText);
                }
                _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
            }
        }

        [Then("I should see the error message for duplicate data")]  //Validation for duplicate data
        public void ThenIShouldSeeTheErrorMessageForDuplicateData()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }

        [When("I enter certification details from json file after the session has expired with the TestName {string}")]  //Add certifications when session expired
        public void WhenIEnterCertificationDetailsFromJsonFileAfterTheSessionHasExpiredWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItems in scenario.TestItems)
            {
                var certificationDetails = testItems.CertificationDetailsToAdd;
                _certificationsAddComponent.ExpireSession();
                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var errorMessage = _certificationsAddComponent.GetErrorMessage();
                _state.ActualCertificationMessages.Add(errorMessage);
                _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
            }
        }

        [Then("I should see the error message for session expired")]  //Validation for session expired
        public void ThenIShouldSeeTheErrorMessageForSessionExpired()
        {
            _state.Assert.ListsMatch(_state.ActualCertificationMessages, _state.ExpectedCertificationMessages);
        }

        [Then("I should see the error message for certificate and provider mismatch")]  //Validation for mismatch data 
        public void ThenIShouldSeeTheErrorMessageForCertificateAndProviderMismatch()
        {
            foreach (var expectedCertificationMessage in _state.ExpectedCertificationMessages)
            {
                _state.Assert.ListContainsString(_state.ActualCertificationMessages, expectedCertificationMessage);
            }
        }

        [When("I enter certification details from json file and cancel the add with the TestName {string}")]  //Cancel add certifications
        public void WhenIEnterCertificationDetailsFromJsonFileAndCancelTheAddWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItems in scenario.TestItems)
            {
                var certificationDetails = testItems.CertificationDetailsToAdd;
                _certificationsAddComponent.CancelAddCertificationDetails(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var actual = _certificationsAddComponent.IsCertificationEmpty(certificationDetails.CertificateOrAward);
                _state.ActualCertificationFlags.Add(actual);
            }
        }

        [Then("I should see the certification details shouldn't be added")]  //Validation for cancel add
        public void ThenIShouldSeeTheCertificationDetailsShouldntBeAdded()
        {
            _state.Assert.IsTrue(_state.ActualCertificationFlags);
        }

        [When("I enter huge Certificate or Award details to perform add from json file with the TestName {string}")] //Add destructive data
        public void WhenIEnterHugeCertificateOrAwardDetailsToPerformAddFromJsonFileWithTheTestName(string fileName)
        {
            var scenario = JsonHelper.ReadJson<CertificationModel>($"TestData/{fileName}.json");

            foreach (var testItems in scenario.TestItems)
            {
                var certificationDetails = testItems.CertificationDetailsToAdd;

                if (certificationDetails.CertificateOrAward.Equals("{LONG_5000}"))
                {
                    certificationDetails.CertificateOrAward = new string('A', 5000);
                }
                _certificationsAddComponent.AddCertifications(certificationDetails.CertificateOrAward, certificationDetails.CertifiedFrom, certificationDetails.Year);
                var successMessage = _certificationsAddComponent.GetSuccessMessage();
                _state.ActualCertificationMessages.Add(successMessage);
                _state.ExpectedCertificationMessages.Add(certificationDetails.CertificationExpectedMessage);
                _state.CleanupCertificationAdd.Add(certificationDetails.CertificateOrAward);
            }
        }
    }
}


