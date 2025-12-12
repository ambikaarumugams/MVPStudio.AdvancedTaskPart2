using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature ="Description")]
    public class DescriptionSteps
    {
        private readonly TestState _state;
        private readonly DescriptionComponent _descriptionComponent;

        public DescriptionSteps(TestState state, DescriptionComponent descriptionComponent)
        {
            _state = state;
            _descriptionComponent = descriptionComponent;
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

        [When("I enter the text in the description using edit icon from the json file {string}")]
        public void WhenIEnterTheTextInTheDescriptionUsingEditIconFromTheJsonFile(string fileName)  //Valid description 
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");
            var descriptionDetails = descriptionItems.DescriptionItems.FirstOrDefault();
            _descriptionComponent.ClickEditIcon();
            _descriptionComponent.EnterTextInTheDescriptionBox(descriptionDetails.DescriptionText);
            var successMessage = _descriptionComponent.GetSuccessMessage();
            _state.ActualDescriptionMessages.Add(successMessage);
            _state.ExpectedDescriptionMessages.Add(descriptionDetails.ExpectedDescriptionMessage);
        }

        [Then("the text should be saved successfully")]
        public void ThenTheTextShouldBeSavedSuccessfully()    //Validation for success
        {
            _state.Assert.ListsMatch(_state.ActualDescriptionMessages, _state.ExpectedDescriptionMessages);
        }

        [When("I enter invalid description text using the edit icon from {string}")]
        public void WhenIEnterInvalidDescriptionTextUsingTheEditIconFrom(string fileName)  //Invalid description
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");

            foreach (var descriptionDetails in descriptionItems.DescriptionItems)
            {
                _descriptionComponent.ClickEditIcon();
                _descriptionComponent.EnterTextInTheDescriptionBox(descriptionDetails.DescriptionText);
                var (messageText, messageType) = _descriptionComponent.GetToastMessage();
                _state.ActualDescriptionTupleMessages.Add((messageType, messageText));
                _state.ExpectedDescriptionMessages.Add(descriptionDetails.ExpectedDescriptionMessage);
            }
        }

        [Then("I should see an appropriate validation error message")]
        public void ThenIShouldSeeAnAppropriateValidationErrorMessage()  //Validation for invalid
        {
            foreach (var expected in _state.ExpectedDescriptionMessages)
            {
                _state.Assert.AssertToastMessageForInvalid(_state.ActualDescriptionTupleMessages, expected);
            }
        }

        [When("I leave the description field empty from the json {string}")]
        public void WhenILeaveTheDescriptionFieldEmptyFromTheJson(string fileName)  //Empty field
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");
            var descriptionDetails = descriptionItems.DescriptionItems.FirstOrDefault();
            _descriptionComponent.ClickEditIcon();
            _descriptionComponent.LeaveTheDescriptionFieldEmpty(descriptionDetails.DescriptionText);
            var actual = _descriptionComponent.GetSuccessMessage();
            _state.ActualDescriptionMessages.Add(actual);
            _state.ExpectedDescriptionMessages.Add(descriptionDetails.ExpectedDescriptionMessage);
        }

        [When("I click the edit icon")]
        public void WhenIClickTheEditIcon()
        {
            _descriptionComponent.ClickEditIcon();
        }

        [When("I read the placeholder text from the description textbox")]
        public void WhenIReadThePlaceholderTextFromTheDescriptionTextbox()
        {
            _descriptionComponent.ClearText();
            var placeHolderText = _descriptionComponent.GetTextAreaPlaceholderText();
            _state.ActualDescriptionMessages.Add(placeHolderText);
        }

        [Then("it should match the expected placeholder text from {string}")]
        public void ThenItShouldMatchTheExpectedPlaceholderTextFrom(string fileName)
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");
            var descriptionDetails = descriptionItems.DescriptionItems.FirstOrDefault();
            _state.ExpectedDescriptionMessages.Add(descriptionDetails.ExpectedDescriptionMessage);
            _state.Assert.ListsMatch(_state.ActualDescriptionMessages, _state.ExpectedDescriptionMessages);
        }

        [When("I enter the text with different range using the edit icon from {string}")]
        public void WhenIEnterTheTextWithDifferentRangeUsingTheEditIconFrom(string fileName)   //Boundary check
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");
            foreach (var item in descriptionItems.DescriptionItems)
            {
                _descriptionComponent.ClickEditIcon();
                _descriptionComponent.EnterTextInTheDescriptionBox(item.DescriptionText);
                var actualText = _descriptionComponent.GetDescriptionText();
                Console.WriteLine(actualText);
                _state.ActualDescriptionMessages.Add(actualText);
                _state.ExpectedDescriptionMessages.Add(item.DescriptionText.ToString());
            }
        }

        [Then("I should see the appropriate validation message")]
        public void ThenIShouldSeeTheAppropriateValidationMessage()
        {
            _state.Assert.ListsMatch(_state.ActualDescriptionMessages, _state.ExpectedDescriptionMessages);
        }

        [When("I enter the text using leading and trailing spaces from the json file {string}")]
        public void WhenIEnterTheTextUsingLeadingAndTrailingSpacesFromTheJsonFile(string fileName)  //Leading and trailing spaces
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");
            var descriptionDetails = descriptionItems.DescriptionItems.FirstOrDefault();
            _descriptionComponent.ClickEditIcon();
            _descriptionComponent.EnterTextInTheDescriptionBox(descriptionDetails.DescriptionText);
            var errorMessage = _descriptionComponent.GetErrorMessage();
            _state.ActualDescriptionMessages.Add(errorMessage);
            _state.ExpectedDescriptionMessages.Add(descriptionDetails.ExpectedDescriptionMessage);
        }

        [When("I enter excessively long data in the description field from the json file {string}")]  //Destructive test
        public void WhenIEnterExcessivelyLongDataInTheDescriptionFieldFromTheJsonFile(string fileName)
        {
            var descriptionItems = JsonHelper.ReadJson<DescriptionModel>($"TestData/{fileName}");
            var descriptionDetails = descriptionItems.DescriptionItems.FirstOrDefault();
            _descriptionComponent.ClickEditIcon();
            if (descriptionDetails.DescriptionText.Equals("{LONG_1000}"))
            {
                descriptionDetails.DescriptionText = new string('a', 1000);
            }
            _descriptionComponent.EnterTextInTheDescriptionBox(descriptionDetails.DescriptionText);
            var actualText = _descriptionComponent.GetDescriptionText();
            Console.WriteLine(actualText);
            _state.ActualDescriptionMessages.Add(actualText);
            _state.ExpectedDescriptionMessages.Add(descriptionDetails.DescriptionText.ToString());

        }

        [Then("I should see the appropriate message")]
        public void ThenIShouldSeeTheAppropriateMessage()
        {
            _state.Assert.ListsMatch(_state.ActualDescriptionMessages, _state.ExpectedDescriptionMessages);
        }
    }
}
