using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using MarsAdvancedTask.Framework.Pages.ManageRequests;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "ManageRequestsReceiveRequest")]
    public class ManageRequestsReceiveRequestSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;
        private readonly SearchComponent _searchComponent;
        private readonly ManageListingsSendRequestComponent _manageListingsSendRequestComponent;
        private readonly ReceiveRequestComponent _receiveRequestComponent;
        public ManageRequestsReceiveRequestSteps(TestState state, ManageListingsEditComponent manageListingsEditComponent, SearchComponent searchComponent, SentRequestComponent sentRequestComponent, ReceiveRequestComponent receiveRequestComponent, ManageListingsSendRequestComponent manageListingsSendRequestComponent)
        {
            _state = state;
            _manageListingsEditComponent = manageListingsEditComponent;
            _searchComponent = searchComponent;
            _manageListingsSendRequestComponent = manageListingsSendRequestComponent;
            _receiveRequestComponent = receiveRequestComponent;
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
            _searchComponent.ClickSellerInfo();
            _searchComponent.ClickTheServiceListing();
            _manageListingsSendRequestComponent.SendRequest(data.Request.Message);
            _searchComponent.ClickYesButton();
            var actualMessage = _searchComponent.GetSuccessMessage();
        }

        [When("I logout")]
        public void WhenILogout()
        {
            _state.SignInComponent.SignOut();
        }

        [Then("I login as {string} from {string}")]
        public void ThenILoginAsFrom(string userKey, string usersFile)
        {
            var users = JsonHelper.ReadJson<UsersData>($"TestData/{usersFile}");
            var user = users.Get(userKey);
            _state.SignInComponent!.SignIn(user.Username, user.Password);
        }

        [Then("I navigate to Manage Requests and open Received Requests")]
        public void ThenINavigateToManageRequestsAndOpenReceivedRequests()
        {
            _receiveRequestComponent.OpenReceiveRequests();
        }

        [When("I accept the received request")]
        public void WhenIAcceptTheReceivedRequest()
        {
            _receiveRequestComponent.ClickAccept();
        }

        [Then("I should see the received request as {string}")]
        public void ThenIShouldSeeTheReceivedRequestAs(string expected)
        {
            var actual = _searchComponent.GetSuccessMessage();
            Console.WriteLine(actual);
            Assert.That(actual, Does.Contain(expected));
        }

        [When("I decline the received request")]
        public void WhenIDeclineTheReceivedRequest()
        {
            _receiveRequestComponent.ClickDecline();
        }
    }
}


