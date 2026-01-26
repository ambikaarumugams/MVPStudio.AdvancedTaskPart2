using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
    [Scope(Feature = "ManageListingsSendRequest")]
    public class ManageListingsSendRequestSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsSendRequestComponent _manageListingsSendRequestComponent;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;
        private readonly SearchComponent _searchComponent;
        public ManageListingsSendRequestSteps(TestState state, SearchComponent searchComponent, ManageListingsSendRequestComponent manageListingsSendRequestComponent, ManageListingsEditComponent manageListingsEditComponent)
        {
            _state = state;
            _manageListingsSendRequestComponent = manageListingsSendRequestComponent;
            _manageListingsEditComponent = manageListingsEditComponent;
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
                _manageListingsEditComponent.AddShareSkills(add);  //Add skill
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(5000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings
                _state.ExpectedManageListingsEdit.Add(add.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(add.Title);
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
            _state.ActualMessageForSendRequest.Add(actualMessage);
            _state.ExpectedMessageForSendRequest.Add(data.Expected.SuccessMessage);
            _state.CleanupForSendRequest.Add(data.Search.Category);
        }

        [Then("I should see trade request success message")]
        public void ThenIShouldSeeTradeRequestSuccessMessage()
        {
            _state.Assert.ListsMatch(_state.ActualMessageForSendRequest, _state.ExpectedMessageForSendRequest);
        }

        [Then("I should see the trade request success message")]
        public void ThenIShouldSeeTheTradeRequestSuccessMessage()
        {
            _state.Assert.ListsMatch(_state.ActualMessageForSendRequest, _state.ExpectedMessageForSendRequest);
        }

        [When("I navigate to the received trade requests page")]
        public void WhenINavigateToTheReceivedTradeRequestsPage()
        {
            _manageListingsSendRequestComponent.ClickNotificationLink();
            _manageListingsSendRequestComponent.ClickSeeAll();
            var actual = _manageListingsSendRequestComponent.GetPendingTradeRequestMessage();
            Console.WriteLine(actual);
        }

        [Then("I should see the trade request from the notifications")]
        public void ThenIShouldSeeTheTradeRequestFromTheNotifications()
        {
            var text = _manageListingsSendRequestComponent.GetPendingTradeRequestMessage();
            Assert.That(text, Does.Contain("Pending trade request from Petesmission"), "Trade request notification content was not found.");
        }

        [When("I open my own listing")]
        public void WhenIOpenMyOwnListing()
        {
            _manageListingsEditComponent.ClickManageListingsLink();
            _manageListingsEditComponent.ClickViewButton();
        }

        [Then("the Send Request button should be disabled")]
        public void ThenTheSendRequestButtonShouldBeDisabled()
        {
            var actual = _manageListingsSendRequestComponent.IsRequestButtonEnabled();
            Assert.That(actual, Is.True, "Request button is enabled");
        }

        [Then("I send a trade request using {string}")]
        public void ThenISendATradeRequestUsing(string tradeFile)
        {
            var data = JsonHelper.ReadJson<ManageListingsSendRequestModel>($"TestData/{tradeFile}");
            _searchComponent.SearchByKeyword(data.Search.Category);
            _searchComponent.OpenListingByTitle(data.Search.Category);
            _searchComponent.ClickListingTitle(data.Search.ListingTitle);
            _manageListingsSendRequestComponent.SendRequest(data.Request.Message);
            _state.ExpectedMessageForSendRequest.Add(data.Search.Category);
        }

        [Then("I shouldn't end the trade request")]
        public void ThenIShouldntEndTheTradeRequest()
        {
            string actualMessage = _manageListingsSendRequestComponent.GetPendingTradeRequestMessage();
            Assert.That(actualMessage, Does.Contain(_state.ExpectedMessageForSendRequest), $"Expected message does not contain '{_state.ExpectedMessageForSendRequest}'");
        }
    }
}


