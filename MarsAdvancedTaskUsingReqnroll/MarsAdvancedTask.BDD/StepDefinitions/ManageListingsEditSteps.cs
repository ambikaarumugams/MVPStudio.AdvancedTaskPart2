using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "ManageListingsEdit")]
    public class ManageListingsEditSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;

        public ManageListingsEditSteps(TestState state, ManageListingsEditComponent manageListingsEditComponent)
        {
            _state = state;
            _manageListingsEditComponent = manageListingsEditComponent;
        }

        [Given("I navigate to the profile page as a registered user")] //Login and navigate to the profile page
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _manageListingsEditComponent.NavigateToTheProfilePage();
        }

        [When("I update the shared skill with skill exchange option using edit icon in the Manage listings from the json file {string}")] //Update shared skill using skill exchange option
        public void WhenIUpdateTheSharedSkillWithSkillExchangeOptionUsingEditIconInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(5000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updatedSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                _state.ActualManageListingsEdit.Add(updatedSuccessMessage);
                Console.WriteLine($"Message:{updatedSuccessMessage}");
                Thread.Sleep(5000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                Thread.Sleep(5000);
                _manageListingsEditComponent.ClickCancel();

                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [Then("the skills should be updated successfully")]
        public void ThenTheSkillsShouldBeUpdatedSuccessfully()
        {
            _state.Assert.ListsMatch(_state.ActualManageListingsEdit, _state.ExpectedManageListingsEdit);
        }

        [When("I update the shared skill with credit option in the Manage listings from the json file {string}")]  //Update shared skill using credit option
        public void WhenIUpdateTheSharedSkillWithCreditOptionInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(6000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updatedSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                _state.ActualManageListingsEdit.Add(updatedSuccessMessage);
                Console.WriteLine($"Message:{updatedSuccessMessage}");
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();

                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill title using random strings in the Manage listings from the json file {string}")]   //Update shared skill title using random strings
        public void WhenIUpdateTheSharedSkillTitleUsingRandomStringsInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updatedSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                _state.ActualManageListingsEdit.Add(updatedSuccessMessage);
                Console.WriteLine($"Message:{updatedSuccessMessage}");
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                Thread.Sleep(5000);
                _manageListingsEditComponent.ClickCancel();

                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared  title using special characters in the Manage listings from the json file {string}")] //Update shared skill title using special characters
        public void WhenIUpdateTheSharedTitleUsingSpecialCharactersInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _state.CleanupManageListingsEdit.Add(add.Title);

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updateErrorMessage = _manageListingsEditComponent.GetErrorMessage();
                Console.WriteLine($"Pop up Message:{updateErrorMessage}");
                _state.ActualManageListingsEdit.Add(updateErrorMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                var fieldErrorText = _manageListingsEditComponent.GetTextOfTitleFieldErrorMessage();
                Console.Write($"Field error message:{fieldErrorText}");
                _state.ActualFieldMessagesForManageListingsEdit.Add(fieldErrorText);
                _state.ExpectedFieldMessagesForManageListingsEdit.Add(update.ExpectedFieldErrorMessage);
                _manageListingsEditComponent.ClickCancel();
            }
        }

        [When("I update the shared skill title with first character as a white space in the Manage listings from the json file {string}")]  //Update shared skill title using first character as a white space
        public void WhenIUpdateTheSharedSkillTitleWithFirstCharacterAsAWhiteSpaceInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _state.CleanupManageListingsEdit.Add(add.Title);

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update);  //Update skill
                var updateErrorMessage = _manageListingsEditComponent.GetErrorMessage();
                Console.WriteLine($"Pop up Message:{updateErrorMessage}");
                _state.ActualManageListingsEdit.Add(updateErrorMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                var fieldErrorText = _manageListingsEditComponent.GetTextOfTitleFieldErrorMessage();
                Console.Write($"Field error message:{fieldErrorText}");
                _state.ActualFieldMessagesForManageListingsEdit.Add(fieldErrorText);
                _state.ExpectedFieldMessagesForManageListingsEdit.Add(update.ExpectedFieldErrorMessage);
                _manageListingsEditComponent.ClickCancel();
            }
        }

        [When("I update the shared skill title with first character as a number in the Manage listings from the json file {string}")] //Update shared skill title using numbers
        public void WhenIUpdateTheSharedSkillTitleWithFirstCharacterAsANumberInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                Thread.Sleep(5000);
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill title in the Manage listings from the json file {string}")] //Update shared skill title boundary testing >100,<100,=100
        public void WhenIUpdateTheSharedSkillTitleInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);
                _manageListingsEditComponent.UpdateShareSkills(update);
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill description with the special characters from the json file {string}")]  //Update shared skill description with special characters
        public void WhenIUpdateTheSharedSkillDescriptionWithTheSpecialCharactersFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _state.CleanupManageListingsEdit.Add(add.Title);

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update);  //Update skill
                var updateErrorMessage = _manageListingsEditComponent.GetErrorMessage();
                Console.WriteLine($"Pop up Message:{updateErrorMessage}");
                _state.ActualManageListingsEdit.Add(updateErrorMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                var fieldErrorText = _manageListingsEditComponent.GetTextOfTitleFieldErrorMessage();
                Console.Write($"Field error message:{fieldErrorText}");
                _state.ActualFieldMessagesForManageListingsEdit.Add(fieldErrorText);
                _state.ExpectedFieldMessagesForManageListingsEdit.Add(update.ExpectedFieldErrorMessage);
                _manageListingsEditComponent.ClickCancel();
            }
        }

        [When("I update the shared skill description with the random strings from the json file {string}")] //Update shared skill description with random strings
        public void WhenIUpdateTheSharedSkillDescriptionWithTheRandomStringsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill description with the numbers from the json file {string}")]  //Update shared skill description using numbers
        public void WhenIUpdateTheSharedSkillDescriptionWithTheNumbersFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _manageListingsEditComponent.ClickCancel();

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill description with first character as a white space from the json file {string}")] //Update shared skill description using first character as a white space
        public void WhenIUpdateTheSharedSkillDescriptionWithFirstCharacterAsAWhiteSpaceFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _manageListingsEditComponent.ClickCancel();

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update); //Update skill
                var updateErrorMessage = _manageListingsEditComponent.GetErrorMessage();
                Console.WriteLine($"Pop up Message:{updateErrorMessage}");
                _state.ActualManageListingsEdit.Add(updateErrorMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                var fieldErrorText = _manageListingsEditComponent.GetTextOfTitleFieldErrorMessage();
                Console.Write($"Field error message:{fieldErrorText}");
                _state.ActualFieldMessagesForManageListingsEdit.Add(fieldErrorText);
                _state.ExpectedFieldMessagesForManageListingsEdit.Add(update.ExpectedFieldErrorMessage);
                _manageListingsEditComponent.ClickCancel();
            }
        }

        [When("I update the shared skill description in the Manage listings from the json file {string}")]  //Update shared skill boundary testing >600,<600,=600 characters
        public void WhenIUpdateTheSharedSkillDescriptionInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);   //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateShareSkills(update);  //Update skill
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill skill exchange tags with invalid input from the json file {string}")]  //Invalid skill exchange tags
        public void WhenIUpdateTheSharedSkillSkillExchangeTagsWithInvalidInputFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateSkillExchangeTags(update);  //Update skill
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill tags with invalid input from the json file {string}")]   //Invalid tags
        public void WhenIUpdateTheSharedSkillTagsWithInvalidInputFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add); //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.UpdateTags(update); //Update skill
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                Thread.Sleep(6000);
                var updatedErrorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{updatedErrorMessage}");
                _manageListingsEditComponent.ClickCancel();
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill tags with with leave either one or all the fields empty from the json file {string}")]  //Update shared skill with Leave either one or all the fields are empty
        public void WhenIUpdateTheSharedSkillTagsWithWithLeaveEitherOneOrAllTheFieldsEmptyFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _manageListingsEditComponent.ClickCancel();

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.LeaveEitherOneOrAllRequiredFieldsEmptyForUpdate(update); //Update skill
                var updateErrorMessage = _manageListingsEditComponent.GetErrorMessage();
                Console.WriteLine($"Pop up Message:{updateErrorMessage}");
                _state.ActualManageListingsEdit.Add(updateErrorMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.ExpectedFieldMessagesForManageListingsEdit.Add(update.ExpectedFieldErrorMessage);
                _manageListingsEditComponent.ClickCancel();
            }
        }

        [Then("the user should see the error message")]
        public void ThenTheUserShouldSeeTheErrorMessage()
        {
            _state.Assert.ListsMatch(_state.ActualManageListingsEdit, _state.ExpectedManageListingsEdit);
            _state.Assert.AssertListContainsAll(_state.ActualFieldMessagesForManageListingsEdit, _state.ExpectedFieldMessagesForManageListingsEdit);
        }

        [Then("the skills shouldn't be updated successfully")]
        public void ThenTheSkillsShouldntBeUpdatedSuccessfully()
        {
            _state.Assert.ListsMatch(_state.ActualManageListingsEdit, _state.ExpectedManageListingsEdit);
        }
    }
}
