using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using NUnit.Framework;
using Reqnroll;

namespace MarsAdvancedTask.BDD.StepDefinitions
{
    [Binding]
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

        [When("I update the shared skill with skill exchange option using edit icon in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedSkillWithSkillExchangeOptionUsingEditIconInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);

                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);

                foreach (var skillTag in add.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                foreach (var workSample in add.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(6000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);

                foreach (var skillTag in update.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                foreach (var workSample in update.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
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

        [Then("the skills should be updated successfully")]
        public void ThenTheSkillsShouldBeUpdatedSuccessfully()
        {
            _state.Assert.ListsMatch(_state.ActualManageListingsEdit, _state.ExpectedManageListingsEdit);
        }

        [When("I update the shared skill with credit option in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedSkillWithCreditOptionInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);

                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);
                _manageListingsEditComponent.SetCreditAmount(add.Credit);

                foreach (var workSample in add.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");
                Thread.Sleep(6000);
                var errorMessage = _manageListingsEditComponent.GetWorkSamplesErrorText();
                Console.WriteLine($"Error message for worksamples:{errorMessage}");
                Thread.Sleep(6000);
                _manageListingsEditComponent.ClickCancel();  //Click the cancel button to delete from manage listings

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);
                _manageListingsEditComponent.SetCreditAmount(update.Credit);

                foreach (var workSample in update.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
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

        [When("I update the shared skill title using random strings in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedSkillTitleUsingRandomStringsInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);

                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);

                foreach (var skillTag in add.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);

                foreach (var skillTag in update.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
                var updatedSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                _state.ActualManageListingsEdit.Add(updatedSuccessMessage);
                Console.WriteLine($"Message:{updatedSuccessMessage}");
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared  title using special characters in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedTitleUsingSpecialCharactersInTheManageListingsFromTheJsonFile(string fileName)
        {

            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);
                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }

                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);

                foreach (var skillTag in add.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                foreach (var workSample in add.WorkSamples)
                {
                    var fullPath = Path.GetFullPath(workSample);
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("File not found:" + fullPath);
                    }
                    _manageListingsEditComponent.UploadWorkSample(fullPath);
                }
                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _state.CleanupManageListingsEdit.Add(add.Title);

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);

                foreach (var skillTag in update.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
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

        [When("I update the shared skill title with first character as a white space in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedSkillTitleWithFirstCharacterAsAWhiteSpaceInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);
                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }

                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);

                foreach (var skillTag in add.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");
                _state.CleanupManageListingsEdit.Add(add.Title);

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);
                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);

                foreach (var skillTag in update.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
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

        [When("I update the shared skill title with first character as a number in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedSkillTitleWithFirstCharacterAsANumberInTheManageListingsFromTheJsonFile(string fileName)
        {

            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);
                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }

                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);

                foreach (var skillTag in add.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);

                foreach (var skillTag in update.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
            }
        }

        [When("I update the shared skill title with  characters in the Manage listings from the json file {string}")]
        public void WhenIUpdateTheSharedSkillTitleWithCharactersInTheManageListingsFromTheJsonFile(string fileName)
        {
            var shareSkillsDetails = JsonHelper.ReadJson<ShareSkillModel>($"TestData/{fileName}");

            for (int i = 0; i < shareSkillsDetails.ShareSkills.Count; i++)
            {
                var add = shareSkillsDetails.ShareSkills[i];
                var update = shareSkillsDetails.EditShareSkills[i];

                _manageListingsEditComponent.ScrollToCenterOfThePage();
                _manageListingsEditComponent.ClickShareSkill();
                _manageListingsEditComponent.EnterTitle(add.Title);
                _manageListingsEditComponent.EnterDescription(add.Description);
                _manageListingsEditComponent.SelectCategory(add.Category);
                _manageListingsEditComponent.SelectSubCategory(add.SubCategory);
                foreach (var tag in add.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }

                _manageListingsEditComponent.SelectServiceType(add.ServiceType);
                _manageListingsEditComponent.SelectLocationType(add.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(add.SkillTradeType);

                foreach (var skillTag in add.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }

                _manageListingsEditComponent.SetActiveStatus(add.Active);
                _manageListingsEditComponent.ClickSave();
                var successMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{successMessage}");

                _manageListingsEditComponent.ClickManageListingsLink();
                _manageListingsEditComponent.ClickEditIcon(add.Title);

                _manageListingsEditComponent.ScrollToCenterOfThePage();  //Scroll the page to the center to make share skill visible

                _manageListingsEditComponent.EnterTitle(update.Title);
                _manageListingsEditComponent.EnterDescription(update.Description);
                _manageListingsEditComponent.SelectCategory(update.Category);
                _manageListingsEditComponent.SelectSubCategory(update.SubCategory);

                foreach (var tag in update.Tags)
                {
                    _manageListingsEditComponent.AddTag(tag);
                }
                _manageListingsEditComponent.SelectServiceType(update.ServiceType);
                _manageListingsEditComponent.SelectLocationType(update.LocationType);

                _manageListingsEditComponent.ClickCalendarAndSelectCurrentDate();
                _manageListingsEditComponent.ClickWeekLink();
                _manageListingsEditComponent.SelectSkillTradeType(update.SkillTradeType);

                foreach (var skillTag in update.SkillExchangeTags)
                {
                    _manageListingsEditComponent.AddSkillExchangeTag(skillTag);
                }
                _manageListingsEditComponent.SetActiveStatus(update.Active);
                _manageListingsEditComponent.ClickSave();
                var updateSuccessMessage = _manageListingsEditComponent.GetSuccessMessage();
                Console.WriteLine($"Pop up Message:{updateSuccessMessage}");
                _state.ActualManageListingsEdit.Add(updateSuccessMessage);
                _state.ExpectedManageListingsEdit.Add(update.ExpectedToastMessage);
                _state.CleanupManageListingsEdit.Add(update.Title);
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
