using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "ManageListingsDelete")]
    public class ManageListingsDeleteSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsDeleteComponent _manageListingsDeleteComponent;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;

        public ManageListingsDeleteSteps(TestState state, ManageListingsDeleteComponent manageListingsDeleteComponent, ManageListingsEditComponent manageListingsEditComponent)
        {
            _state = state;
            _manageListingsDeleteComponent = manageListingsDeleteComponent;
            _manageListingsEditComponent = manageListingsEditComponent;
        }

        [Given("I navigate to the profile page as a registered user")]
        public void GivenINavigateToTheProfilePageAsARegisteredUser()
        {
            Console.WriteLine($"Test: {TestContext.CurrentContext.Test.Name}," +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}," +
                              $"StateId: {_state.InstanceId}");   //To check whether it's sharing same test state instance or using different one for each test method
            var loginDetails = JsonHelper.ReadJson<LoginModel>("TestData/LoginData.json");
            _state.SignInComponent.SignIn(loginDetails.Username, loginDetails.Password);
            _manageListingsEditComponent.NavigateToTheProfilePage();
        }

        [When("I add a shared skill from {string} and delete it from Manage Listings")]
        public void WhenIAddASharedSkillFromAndDeleteItFromManageListings(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                _manageListingsEditComponent.AddShareSkills(add);
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(5000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings


                _manageListingsEditComponent.ClickManageListingsLink();
                var delete = shareSkillsDetails.DeleteShareSkills[i];
                _state.ActualManageListingsTitleToDelete.Add(delete.Title);
                foreach (var title in _state.ActualManageListingsTitleToDelete)
                {
                    _manageListingsDeleteComponent.DeleteSpecificSharedSkill(title);
                    var successMessageForDelete = _manageListingsEditComponent.GetSuccessMessage();
                    Console.WriteLine($"Message:{successMessageForDelete}");
                    _state.ActualManageListingsDelete.Add(successMessageForDelete);
                }
                _state.ExpectedManageListingsDelete.Add(delete.ExpectedToastMessage);
                _state.ActualManageListingsTitleFromTable.Add(delete.Title);
            }
        }

        [Then("I should see the delete success message")]
        public void ThenIShouldSeeTheDeleteSuccessMessage()
        {
            _state.Assert.ListsMatch(_state.ActualManageListingsDelete, _state.ExpectedManageListingsDelete);
        }

        [Then("the deleted skill should no longer appear in Manage Listings")]
        public void ThenTheDeletedSkillShouldNoLongerAppearInManageListings()
        {
            _manageListingsEditComponent.ClickManageListingsLink();
            var tableResult = _manageListingsDeleteComponent.IsSkillPresent();
            Assert.That(tableResult, Is.False);
        }
    }
}
