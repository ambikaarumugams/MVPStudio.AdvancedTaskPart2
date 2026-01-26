using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{

    [Binding]
    [NonParallelizable]
    [Scope(Feature = "ManageListingsView")]
    public class ManageListingsViewSteps
    {
        private readonly TestState _state;
        private readonly ManageListingsViewComponent _manageListingsViewComponent;
        private readonly ManageListingsEditComponent _manageListingsEditComponent;

        public ManageListingsViewSteps(TestState state, ManageListingsViewComponent manageListingsViewComponent, ManageListingsEditComponent manageListingsEditComponent)
        {
            _state = state;
            _manageListingsEditComponent = manageListingsEditComponent;
            _manageListingsViewComponent = manageListingsViewComponent;
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

        [When("I save a new shared skill using {string}")]
        public void WhenISaveANewSharedSkillUsing(string fileName)
        {
            var testItems = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            foreach (var skill in testItems.ShareSkills)
            {
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(skill.Title);
                _manageListingsEditComponent.EnterDescription(skill.Description);
                _manageListingsEditComponent.SelectCategory(skill.Category);
                _manageListingsEditComponent.SelectSubCategory(skill.SubCategory);

                foreach (var tag in skill.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(skill.ServiceType);
                _manageListingsEditComponent.SelectLocationType(skill.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(skill.SkillTradeType);

                foreach (var skillTag in skill.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                foreach (var workSample in skill.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(skill.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(6000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _state.CleanupManageListings.Add(skill.Title);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings
            }
        }

        [When("I click the Manage Listings tab")]
        public void WhenIClickTheManageListingsTab()
        {
            _manageListingsViewComponent.ClickManageListingsTab();
        }

        [Then("the added skill should be displayed in the Manage Listings page")]
        public void ThenTheAddedSkillShouldBeDisplayedInTheManageListingsPage()
        {
            var expectedList = _manageListingsViewComponent.GetManageListingsTable();
            Console.WriteLine(expectedList.ToString());
            foreach (var expected in expectedList)
            {
                var expectedString = string.Join(",", expected);
                _state.ExpectedManageListingsView.Add(expectedString);
            }
        }

        [Then("I open the skill using the View icon")]
        public void ThenIOpenTheSkillUsingTheViewIcon()
        {
            var actualList = _manageListingsViewComponent.ClickAllEyeIcons();
            Console.WriteLine(actualList.ToString());
            var actualString = string.Join(" , ", actualList);
            Console.WriteLine(actualString);
            _state.ActualManageListingsView.Add(actualString);
        }

        [Then("I should be able to validate the displayed skill details")]
        public void ThenIShouldBeAbleToValidateTheDisplayedSkillDetails()
        {
            foreach (var expected in _state.ExpectedManageListingsView)
            {
                _state.Assert.ListContainsString(_state.ActualManageListingsView, expected);   //Need to work on 
            }
        }

        [When("I save the new skill using {string} and view the skill using view icon")]
        public void WhenISaveTheNewSkillUsingAndViewTheSkillUsingViewIcon(string fileName)
        {
            var testItems = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            foreach (var skill in testItems.ShareSkills)
            {
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(skill.Title);
                _manageListingsEditComponent.EnterDescription(skill.Description);
                _manageListingsEditComponent.SelectCategory(skill.Category);
                _manageListingsEditComponent.SelectSubCategory(skill.SubCategory);

                foreach (var tag in skill.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(skill.ServiceType);
                _manageListingsEditComponent.SelectLocationType(skill.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(skill.SkillTradeType);

                foreach (var skillTag in skill.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                foreach (var workSample in skill.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(skill.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(6000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _state.CleanupManageListings.Add(skill.Title);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings
            }
        }

        [When("I click the chat box to communicate with others")]
        public void WhenIClickTheChatBoxToCommunicateWithOthers()
        {
            _manageListingsViewComponent.ClickManageListingsTab();
            _manageListingsViewComponent.ViewManageListings();
            var result = _manageListingsViewComponent.IsChatEnabled();
            Console.WriteLine($"Is chat button enabled:{result}");
            _manageListingsViewComponent.ClickChat();
            var message = "Hi, I'm interested in your listings...";  //Input for chat box
            _manageListingsViewComponent.EnterMessageInChatBox(message);
            _state.ExpectedManageListingsView.Add(message);
            _manageListingsViewComponent.ClickSendButton();
            var actual = _manageListingsViewComponent.GetChatMessage();
            _state.ActualManageListingsView.Add(actual);
        }

        [Then("I should be able to send a message successfully")]
        public void ThenIShouldBeAbleToSendAMessageSuccessfully()
        {
            _state.Assert.ListsMatch(_state.ActualManageListingsView, _state.ExpectedManageListingsView);
        }

        [When("I click the request button to send request to others")]
        public void WhenIClickTheRequestButtonToSendRequestToOthers()
        {
            _manageListingsViewComponent.ClickManageListingsTab();
            _manageListingsViewComponent.ViewManageListings();
            var actual = _manageListingsViewComponent.IsRequestEnabled();
            Console.WriteLine($"Is request button enabled:{actual}");
        }

        [Then("I should be able to send a request if request button is enabled")]
        public void ThenIShouldBeAbleToSendARequestIfRequestButtonIsEnabled()
        {
            Assert.Fail();
        }
    }
}
