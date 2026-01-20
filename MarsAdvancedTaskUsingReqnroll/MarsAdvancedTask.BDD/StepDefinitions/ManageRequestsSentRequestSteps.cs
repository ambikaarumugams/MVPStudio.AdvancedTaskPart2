using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using MarsAdvancedTask.Framework.Pages.ManageRequests;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "ManageRequestsSentRequest")]
    public class ManageRequestsSentRequestSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;
        private readonly SearchComponent _searchComponent;
        private readonly ManageListingsSendRequestComponent _manageListingsSendRequestComponent;
        private readonly SentRequestComponent _sentRequestComponent;
        public ManageRequestsSentRequestSteps(TestState state, ManageListingsEditComponent manageListingsEditComponent, SearchComponent searchComponent, SentRequestComponent sentRequestComponent, ManageListingsSendRequestComponent manageListingsSendRequestComponent)
        {
            _state = state;
            _manageListingsEditComponent = manageListingsEditComponent;
            _searchComponent = searchComponent;
            _manageListingsSendRequestComponent = manageListingsSendRequestComponent;
            _sentRequestComponent = sentRequestComponent;
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
                _state.CleanupSentRequest.Add(add.Title);
            }
        }

        [Given("I logout")]
        public void GivenILogout()
        {
            _state.SignInComponent.SignOut();
        }

        [When("I login as {string} from {string}")]
        public void WhenILoginAsFrom(string userKey, string usersFile)
        {
            var users = JsonHelper.ReadJson<UsersData>($"TestData/{usersFile}");
            var user = users.Get(userKey);
            _state.SignInComponent!.SignIn(user.Username, user.Password);
        }

        [When("I send a trade request using {string}")]
        public void WhenISendATradeRequestUsing(string tradeFile)
        {
            var data = JsonHelper.ReadJson<ManageListingsSendRequestModel>($"TestData/{tradeFile}");
            _searchComponent.SearchByKeyword(data.Search.Category);
            _searchComponent.OpenListingByTitle(data.Search.Category);
            _searchComponent.ClickListingTitle(data.Search.ListingTitle);
            _manageListingsSendRequestComponent.SendRequest(data.Request.Message);
            _searchComponent.ClickYesButton();
            var actualMessage = _searchComponent.GetSuccessMessage();
        }

        [When("I navigate to Manage Requests")]
        public void WhenINavigateToManageRequests()
        {
            _sentRequestComponent.HoverOnElement();
        }

        [When("I open Sent Requests")]
        public void WhenIOpenSentRequests()
        {
            _sentRequestComponent.OpenSentRequests();
        }

        [Then("I should see the sent request as {string}")]
        public void ThenIShouldSeeTheSentRequestAs(string expected)
        {
            var actual = _sentRequestComponent.GetDataFromTable();
            _state.ActualSentRequest.Add(actual);
            _state.Assert.ListContainsString(_state.ActualSentRequest, expected);
        }
    }
}


