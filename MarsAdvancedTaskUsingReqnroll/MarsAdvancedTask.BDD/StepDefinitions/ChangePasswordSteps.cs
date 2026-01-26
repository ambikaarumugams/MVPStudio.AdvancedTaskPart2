using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [NonParallelizable]
    [Scope(Feature = "ChangePassword")]
    public class ChangePasswordSteps
    {
        private readonly TestState _state;
        private readonly ChangePasswordComponent _changePasswordComponent;

        public ChangePasswordSteps(TestState state, ChangePasswordComponent changePasswordComponent)
        {
            _state = state;
            _changePasswordComponent = changePasswordComponent;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
        }

        [When("I change the password using valid details from the json file {string}")]
        public void WhenIChangeThePasswordUsingValidDetailsFromTheJsonFile(string fileName)   //Change password using valid details
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            if (_state.ActualChangePasswordMessage != null)
                _state.ActualChangePasswordMessage.Add(actualMessage);
            if (_state.ExpectedChangePasswordMessage != null)
                _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [Then("the password should be changed successfully")]   //Validation for success
        public void ThenThePasswordShouldBeChangedSuccessfully()
        {
            _state.Assert.ListsMatch(_state.ActualChangePasswordMessage, _state.ExpectedChangePasswordMessage);
        }

        //[When("I try to change the password again using the previous password from the json file {string}")]
        //public void WhenITryToChangeThePasswordAgainUsingThePreviousPasswordFromTheJsonFile(string fileName)
        //{
        //    var changePasswordDetails = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");

        //    _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
        //    var actualMessage = _changePasswordComponent.GetSuccessMessage();
        //    _state.ActualChangePasswordMessage.Add(actualMessage);
        //    _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        //}

        [When("I enter an incorrect current password and try to save from the json file {string}")]
        public void WhenIEnterAnIncorrectCurrentPasswordAndTryToSaveFromTheJsonFile(string fileName)  //Incorrect current password
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetErrorMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [Then("the password should not be changed")]
        public void ThenThePasswordShouldNotBeChanged()
        {
            _state.Assert.ListsMatch(_state.ActualChangePasswordMessage, _state.ExpectedChangePasswordMessage);
        }

        [When("I enter the current password as the new password from the json file {string}")]
        public void WhenIEnterTheCurrentPasswordAsTheNewPasswordFromTheJsonFile(string fileName)  //Current password as new password
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetErrorMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [When("I enter different values for the new password and confirm password from the json file {string}")]
        public void WhenIEnterDifferentValuesForTheNewPasswordAndConfirmPasswordFromTheJsonFile(string fileName)  //New password and confirm password mismatch
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetErrorMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [When("I enter a weak password without uppercase letters, numbers or special characters from the json file {string}")]
        public void WhenIEnterAWeakPasswordWithoutUppercaseLettersNumbersOrSpecialCharactersFromTheJsonFile(string fileName)  //Weak password
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [Then("I should see password strength warnings")]
        public void ThenIShouldSeePasswordStrengthWarnings()
        {
            _state.Assert.ListsMatch(_state.ActualChangePasswordMessage, _state.ExpectedChangePasswordMessage);
        }

        [When("I enter random strings as the new password and confirm password from the json file {string}")]
        public void WhenIEnterRandomStringsAsTheNewPasswordAndConfirmPasswordFromTheJsonFile(string fileName)  //Random strings
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [When("I enter only numbers as the new password and confirm password from the json file {string}")]
        public void WhenIEnterOnlyNumbersAsTheNewPasswordAndConfirmPasswordFromTheJsonFile(string fileName)  //Random numbers
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [When("I enter only special characters as the new password and confirm password from the json file {string}")]
        public void WhenIEnterOnlySpecialCharactersAsTheNewPasswordAndConfirmPasswordFromTheJsonFile(string fileName)   //Special characters
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [When("I leave one or more password fields empty from the json file {string}")]
        public void WhenILeaveOneOrMorePasswordFieldsEmptyFromTheJsonFile(string fileName)   //Empty fields either one or all
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            foreach (var changePasswordDetails in testItems.TestItems)
            {
                _changePasswordComponent.LeaveEitherOneOrAllTheFieldsEmpty(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
                var actualMessage = _changePasswordComponent.GetErrorMessage();
                _state.ActualChangePasswordMessage.Add(actualMessage);
                _state.Driver.Navigate().Refresh();  //To refresh the page so that I can do another entry
                _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
            }
        }

        [When("I enter a password to test the minimum length requirement from the json file {string}")]
        public void WhenIEnterAPasswordToTestTheMinimumLengthRequirementFromTheJsonFile(string fileName) //Minimum length
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetErrorMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            Console.WriteLine("Length of the Password:" + changePasswordDetails.NewPassword.Length); //Length of the password
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [Then("I should see validation related to minimum length")]
        public void ThenIShouldSeeValidationRelatedToMinimumLength()
        {
            _state.Assert.ListsMatch(_state.ActualChangePasswordMessage, _state.ExpectedChangePasswordMessage);
        }

        [When("I enter a password to test the maximum length requirement from the json file {string}")]
        public void WhenIEnterAPasswordToTestTheMaximumLengthRequirementFromTheJsonFile(string fileName)   //Maximum length
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            Console.WriteLine("Length of the Password:" + changePasswordDetails.NewPassword.Length); //Length of the password
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [Then("I should see validation related to maximum length")]
        public void ThenIShouldSeeValidationRelatedToMaximumLength()
        {
            _state.Assert.ListsMatch(_state.ActualChangePasswordMessage, _state.ExpectedChangePasswordMessage);
        }

        [When("I enter a password with leading or trailing spaces from the json file {string}")]
        public void WhenIEnterAPasswordWithLeadingOrTrailingSpacesFromTheJsonFile(string fileName)  //Leading and trailing spaces
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetSuccessMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            Console.WriteLine("Length of the Password:" + changePasswordDetails.NewPassword.Length); //Length of the password
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }

        [When("I try to change the password after the session has expired from the json file {string}")]
        public void WhenITryToChangeThePasswordAfterTheSessionHasExpiredFromTheJsonFile(string fileName)  //When session has expired
        {
            var testItems = JsonHelper.ReadJson<ChangePasswordModel>($"TestData/{fileName}");
            var changePasswordDetails = testItems.TestItems.First();
            _changePasswordComponent.ExpireSession();
            _changePasswordComponent.ChangePassword(changePasswordDetails.CurrentPassword, changePasswordDetails.NewPassword, changePasswordDetails.ConfirmPassword);
            var actualMessage = _changePasswordComponent.GetErrorMessage();
            _state.ActualChangePasswordMessage.Add(actualMessage);
            Console.WriteLine("Length of the Password:" + changePasswordDetails.NewPassword.Length); //Length of the password
            _state.ExpectedChangePasswordMessage.Add(changePasswordDetails.ExpectedMessage);
        }
    }
}
