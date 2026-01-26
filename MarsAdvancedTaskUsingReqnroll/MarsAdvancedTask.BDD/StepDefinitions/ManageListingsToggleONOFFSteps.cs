using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "ManageListingsToggleONOFF")]
    public class ManageListingsToggleONOFFSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;
        private readonly ManageListingsToggleONOFFComponent _manageListingsToggleONOFFComponent;
        private readonly SearchComponent _searchComponent;
        public ManageListingsToggleONOFFSteps(TestState state, ManageListingsEditComponent manageListingsEditComponent, ManageListingsToggleONOFFComponent manageListingsToggleONOFFComponent, SearchComponent searchComponent)
        {
            _state = state;
            _manageListingsEditComponent = manageListingsEditComponent;
            _manageListingsToggleONOFFComponent = manageListingsToggleONOFFComponent;
            _searchComponent = searchComponent;
        }

        [Given("I login as {string} from {string}")]
        public void GivenILoginAsFrom(string userKey, string usersFile)
        {
            var users = JsonHelper.ReadJson<UsersData>($"TestData/{usersFile}");
            var user = users.Get(userKey);
            _state.SignInComponent!.SignIn(user.Username, user.Password);
        }

        [Given("I create a share skill using {string}")]
        public void GivenICreateAShareSkillUsing(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");
            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                _state.ListingSearchByCategory = add.Category; //Store the category to search skills for validation

                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(5000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings
                _state.CleanupToggle.Add(add.Title);
            }
        }

        [When("I navigate to Manage Listings")]
        public void WhenINavigateToManageListings()
        {
            _manageListingsEditComponent.ClickManageListingsLink();
        }

        [When("I toggle the listing status to OFF for the stored listing")]
        public void WhenIToggleTheListingStatusToOFFForTheStoredListing()
        {
            _manageListingsToggleONOFFComponent.ClickToggle();
            var actual = _manageListingsToggleONOFFComponent.GetSuccessMessage();
            Console.WriteLine(actual);
            _state.ActualMessageForToggle.Add(actual);
        }

        [Then("I should see listing status as {string} and other users shouldn't see the listings")]
        public void ThenIShouldSeeListingStatusAsAndOtherUsersShouldntSeeTheListings(string inactive)
        {
            var category = _state.ListingSearchByCategory;
            var isVisible = _searchComponent.IsListingVisibleInSearch(category);
            _state.ExpectedMessageForToggle.Add(inactive);
            _state.Assert.ListsMatch(_state.ActualMessageForToggle, _state.ExpectedMessageForToggle);
            Assert.That(isVisible, Is.False, $"Listing should NOT be visible after deactivation.");

        }

        [When("I toggle the listing status to ON for the stored listing")]
        public void WhenIToggleTheListingStatusToONForTheStoredListing()
        {
            _manageListingsToggleONOFFComponent.ClickToggle();
            var actual = _manageListingsToggleONOFFComponent.GetSuccessMessage();
            Console.WriteLine(actual);
            _state.ActualMessageForToggle.Add(actual);
        }

        [Then("I should see listing status as {string} and other users should see the listings")]
        public void ThenIShouldSeeListingStatusAsAndOtherUsersShouldSeeTheListings(string active)
        {
            var category = _state.ListingSearchByCategory;
            var isVisible = _searchComponent.IsListingVisibleInSearch(category);
            _state.ExpectedMessageForToggle.Add(active);
            _state.Assert.ListsMatch(_state.ActualMessageForToggle, _state.ExpectedMessageForToggle);
            Assert.That(isVisible, Is.True, $"Listing should be visible after activation.");
        }
    }
}


