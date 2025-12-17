using AventStack.ExtentReports;
using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages.ManageListingsComponent
{
    public class ManageListingsEditComponent
    {
        private readonly TestState _state;

        public ManageListingsEditComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _profileTab = By.XPath("//a[normalize-space()='Profile']");
        private readonly By _shareSkillTab = By.XPath("//a[normalize-space()='Share Skill']");
        private readonly By _titleTextField = By.XPath("//input[@placeholder='Write a title to describe the service you provide.']");
        private readonly By _descriptionTextField = By.XPath("//textarea[@placeholder='Please tell us about any hobbies, additional expertise, or anything else you’d like to add.']");
        private readonly By _tagsInputBox = By.XPath("//div[@class='ReactTags__tagInput']/input[contains(@aria-label,'Add new tag')]");
        private readonly By _calendar = By.XPath("//a[@class='k-link']//span[@class='k-icon k-i-calendar']");
        private readonly By _previousArrowToSelectPastDate = By.XPath("//a[@class='k-link k-nav-prev']");
        private readonly By _nextArrowToSelectFutureDate = By.XPath("//a[@class='k-link k-nav-next']");
        private readonly By _calendarHeader = By.XPath("//div[contains(@class,'k-scheduler-calendar')]//a[contains(@class,'k-link') and contains(@class,'k-nav-fast')]");
        private readonly By _selectCategory = By.XPath("//select[@name='categoryId']");
        private readonly By _selectSubCategory = By.XPath("//select[@name='subcategoryId']");

        private readonly By _workWeekTab = By.XPath("//li[contains(@class,'k-current-view')]//a[contains(@role,'button')][normalize-space()='Work Week']");
        private readonly By _dayLink = By.XPath("//a[normalize-space()='Day']");
        private readonly By _workWeekLink = By.XPath("//li[contains(@class,'k-state-default k-view-workweek k-state-selected')]//a[contains(@role,'button')][normalize-space()='Work Week']");
        private readonly By _weekLink = By.XPath("//a[normalize-space()='Week']");
        private readonly By _monthLink = By.XPath("//a[normalize-space()='Month']");
        private readonly By _agendaLink = By.XPath("//a[normalize-space()='Agenda']");
        private readonly By _timeLineLink = By.XPath("//a[normalize-space()='Timeline']");
        private readonly By _eventTitle = By.XPath("//input[@title='Title']");
        private readonly By _startDate = By.XPath("//div[@data-container-for='start']//span[@class='k-widget k-datetimepicker k-header']//span[@class='k-icon k-i-calendar']");
        private readonly By _startDateTime = By.XPath("//div[@data-container-for='start']//span[@class='k-icon k-i-clock']");
        private readonly By _endDate = By.XPath("//div[@data-container-for='end']//span[@class='k-widget k-datetimepicker k-header']//span[@class='k-icon k-i-calendar']");
        private readonly By _endDateTime = By.XPath("//div[@data-container-for='end']//span[@class='k-icon k-i-clock']");
        private readonly By _selectDateInsideEventCreator = By.XPath("//td[contains(@id,'_cell')]//a[@title='{fullDateTitle}' and normalize-space(text())='{day}']");
        private readonly By _alertMessage = By.XPath("//div[@role='alert']");
        private readonly By _allDayEventCheckBox = By.XPath("//input[@title='All day event']");
        private readonly By _repeatEveryTextBox = By.XPath("//input[@class='k-recur-interval k-input']");
        private readonly By _repeatEveryDaysUpArrow = By.XPath("//span[@class='k-numeric-wrap k-state-default']//span[@class='k-icon k-i-arrow-60-up']");
        private readonly By _repeatEveryDaysDownArrow = By.XPath("//span[@class='k-numeric-wrap k-state-default']//span[@class='k-icon k-i-arrow-60-down']");
        private readonly By _endNeverRadioButton = By.XPath("//input[@value='never']");
        private readonly By _endAfterNOcuurencesRadioButton = By.XPath("//input[@value='count']");
        private readonly By _occurenceUpArrow = By.XPath("//span[@class='k-widget k-numerictextbox k-recur-count']//span[@class='k-icon k-i-arrow-60-up']");
        private readonly By _occurenceDownArrow = By.XPath("//span[@class='k-widget k-numerictextbox k-recur-count']//span[@class='k-icon k-i-arrow-60-down']");
        private readonly By _endOnRadioButton = By.XPath("//input[@value='until']");
        private readonly By _endOnDateTextBox = By.XPath("//input[@title='On ']");

        private readonly By _repeatEveryForWeeklyTextBox = By.XPath("//input[@class='k-formatted-value k-recur-interval k-input']");
        private readonly By _repeatDownArrow = By.XPath("//span[@title='Recurrence editor']//span[@class='k-icon k-i-arrow-60-down']");
        private readonly By _descriptionTextBoxInsideEvent = By.XPath("//textarea[@title='Description']");
        private readonly By _ownerTextBoxArrow = By.XPath("//span[@title='No title']//span[@class='k-icon k-i-arrow-60-down']");
        private readonly By _saveEventButton = By.XPath("//a[normalize-space()='Save']");
        private readonly By _cancelEventButton = By.XPath("//a[normalize-space()='Cancel']");
        private readonly By _showBusinessHoursTab = By.XPath("//a[normalize-space()='Show business hours']");
        private readonly By _skillExchangeTagInputBox = By.XPath("//div[contains(@class,'twelve wide column')]//div[contains(@class,'')]//div[contains(@class,'form-wrapper')]//input[contains(@placeholder,'Add new tag')]");
        private readonly By _creditTextBox = By.XPath("//input[@placeholder='Amount']");
        private readonly By _workSamplesIcon = By.XPath("//i[@class='huge plus circle icon padding-25']");
        private readonly By _saveButton = By.XPath("//input[@value='Save']");
        private readonly By _cancelButton = By.XPath("//input[@value='Cancel']");
        private readonly By _titleFieldError = By.XPath("//div[@class='ui basic red prompt label transition visible']");
        private readonly By _descriptionFieldErrorForSpecialCharacters = By.XPath("//div[normalize-space()='Special characters are not allowed.']");
        private readonly By _descriptionFieldError = By.XPath("//div[normalize-space()='Description is required']");
        private readonly By _categoryFieldError = By.XPath("//div[normalize-space()='Category is required']");
        private readonly By _tagsFieldError = By.XPath("//div[contains(text(),'Tags are required')]");
        private readonly By _skillExchangeFieldError = By.XPath("//div[contains(text(),'Tag is required')]");
        private readonly By _manageListingsLink = By.XPath("//a[normalize-space()='Manage Listings']");
        private readonly By _manageListingsTable = By.XPath("//table[@class='ui striped table']");
        private readonly By _viewAddedSkills = By.XPath("//table[@class='ui striped table']//tbody/tr[1]//button[i[contains(@class,'eye icon')]]");
        private readonly By _serviceTypeFromAddedSkills = By.XPath("//div[@class='content'][div[@class='header' and text()='Service Type']]/div[@class='description']");
        private readonly By _locationTypeFromAddedSkills = By.XPath("//div[@class='content'][div[@class='header' and text()='Location Type']]/div[@class='description']");
        //  private readonly By _editIconElements = By.XPath("//tbody/tr/td[8]/div/button[@class='ui button']/i[contains(@class,'outline write icon')]");

        //Action Methods
        public void NavigateToTheProfilePage()
        {
            var profileElement = _state.Wait.WaitUntilElementToBeClickable(_profileTab);
            profileElement.Click();
        }

        public void ClickShareSkill()
        {
            var shareSkillElement = _state.Wait.WaitUntilElementToBeClickable(_shareSkillTab);
            Thread.Sleep(1000);
            shareSkillElement.Click();
        }

        public void EnterTitle(string title)
        {
            var enterTitleTextBox = _state.Wait.WaitUntilElementToBeClickable(_titleTextField);
            enterTitleTextBox.Clear();
            enterTitleTextBox.SendKeys(title);
        }

        public void EnterDescription(string description)
        {
            var enterDescriptionTextBox = _state.Wait.WaitUntilElementToBeClickable(_descriptionTextField);
            enterDescriptionTextBox.Clear();
            enterDescriptionTextBox.SendKeys(description);
        }

        public void SelectCategory(string category)
        {
            var selectCategory = _state.Wait.WaitUntilElementToBeClickable(_selectCategory);
            SelectElement categoryDropDown = new SelectElement(selectCategory);
            categoryDropDown.SelectByText(category);
        }

        public void SelectSubCategory(string subCategory)
        {
            var selectSubCategory = _state.Wait.WaitUntilElementToBeClickable(_selectSubCategory);
            SelectElement subCategoryDropDown = new SelectElement(selectSubCategory);
            subCategoryDropDown.SelectByText(subCategory);
        }

        public void AddTag(string tag)
        {
            var tagsInputBox = _state.Wait.WaitUntilElementToBeClickable(_tagsInputBox);
            tagsInputBox.SendKeys(tag + Keys.Enter);
        }

        public void SelectServiceType(string serviceType)
        {
            var serviceTypeRadioButton = _state.Wait.WaitUntilElementToBeClickable(By.XPath($"//input[@name='serviceType' and @value='{serviceType}']/following-sibling::label"));

            ((IJavaScriptExecutor)_state.Driver).ExecuteScript("arguments[0].scrollIntoView(true);",
                serviceTypeRadioButton); // Scroll into view
            Thread.Sleep(3000);

            if (!serviceTypeRadioButton.Selected)
            {
                serviceTypeRadioButton.Click();
                _state.Test.Log(Status.Info, $"Service Type selected: {(serviceType == "0" ? "Hourly basis" : "One-off service")}");
            }
            else
            {
                _state.Test.Log(Status.Info, $"Service Type already selected: {(serviceType == "0" ? "Hourly basis" : "One-off service")}");
            }
        }

        public void SelectLocationType(string locationType)
        {
            var locationTypeRadioButton = _state.Wait.WaitUntilElementIsVisible(By.XPath($"//input[@name='locationType' and @value='{locationType}']/following-sibling::label"));

            if (!locationTypeRadioButton.Selected)
            {
                locationTypeRadioButton.Click();
                _state.Test.Log(Status.Info, $"Location Type selected: {(locationType == "0" ? "On-site" : "Online")}");
            }
            else
            {
                _state.Test.Log(Status.Info, $"Location Type already selected:{(locationType == "0" ? "On-site" : "Online")}");
            }
        }

        public void ClickCalendarAndSelectCurrentDate() //I used Javascript to select the current date/today's date
        {
            var script = @"const calendarIcon = document.querySelector('a.k-link .k-i-calendar').closest('a');
                           calendarIcon.click();
                           setTimeout(() => {  const todayBtn = document.querySelector('a.k-nav-today');
                                               if (todayBtn) todayBtn.click();
                                             }, 500);"; // Wait 500ms to allow calendar popup to render

            ((IJavaScriptExecutor)_state.Driver).ExecuteScript(script);

            Thread.Sleep(500); // Wait to let it visually update
            _state.Test.Log(Status.Info, "Selected today via chained calendar interaction.");
            _state.Test.Log(Status.Info, "Calendar is active and open.");
        }

        public void ClickWeekLink()
        {
            var weekLinkElement = _state.Wait.WaitUntilElementToBeClickable(_weekLink);
            weekLinkElement.Click();
        }

        public void ClickWorkWeekLink()
        {
            var workWeekLinkElement = _state.Wait.WaitUntilElementToBeClickable(_workWeekLink);
            workWeekLinkElement.Click();
        }

        public string GetCalendarHeader()
        {
            var calendarHeaderElement = _state.Wait.WaitUntilElementIsVisible(_calendarHeader);
            return calendarHeaderElement.Text;
        }

        public void ClickNextArrowForFutureDates()
        {
            var nextArrowElement = _state.Wait.WaitUntilElementToBeClickable(_nextArrowToSelectFutureDate);
            nextArrowElement.Click();
        }

        public void ClickPreviousArrowForPastDates()
        {
            var pastArrowElement = _state.Wait.WaitUntilElementToBeClickable(_previousArrowToSelectPastDate);
            pastArrowElement.Click();
        }

        public void SelectDateFromTheCalender(string day)
        {
            var daysLocator = $"//table[@role='grid']//tbody//tr//td[normalize-space(text())='{day}']";
            var cell = _state.Driver.FindElement(By.XPath(daysLocator));
            cell.Click();
        }

        public void SelectSkillTradeType(string tradeType)
        {
            string value = tradeType.Equals("SkillExchange", StringComparison.OrdinalIgnoreCase) ? "true" : "false";
            string text = tradeType.Equals("SkillExchange", StringComparison.OrdinalIgnoreCase) ? "Skill-exchange" : "Credit";

            var radioButtonLocator = By.XPath($"//input[@name='skillTrades']/following-sibling::label[normalize-space(.)='{text}']");

            var radioButton = _state.Wait.WaitUntilElementToBeClickable(radioButtonLocator);
            ((IJavaScriptExecutor)_state.Driver).ExecuteScript("arguments[0].click();", radioButton);
            Thread.Sleep(300);

            _state.Test.Log(Status.Info, $"Skill Trade Type selected: {tradeType}");
        }

        public void AddSkillExchangeTag(string skillTag)
        {
            var skillExchangeTextBox = _state.Wait.WaitUntilElementIsVisible(_skillExchangeTagInputBox);
            skillExchangeTextBox.SendKeys(skillTag + Keys.Enter);
        }

        public void SetCreditAmount(string amount)
        {
            var creditTextBoxElement = _state.Wait.WaitUntilElementIsVisible(_creditTextBox);
            creditTextBoxElement.Clear();
            creditTextBoxElement.SendKeys(amount);
        }

        public void UploadWorkSample(string filePath)
        {
            var fileInput = _state.Driver.FindElement(By.Id("selectFile"));

            // Optional: Unhide if necessary
            ((IJavaScriptExecutor)_state.Driver).ExecuteScript("arguments[0].style.display = 'block';",
                fileInput); //It makes a hidden HTML element visible by changing its CSS display property.

            fileInput.SendKeys(filePath); // This simulates file selection
            _state.Test.Log(Status.Info, $"Uploaded file: {Path.GetFileName(filePath)}");
        }

        public void SetActiveStatus(string status)
        {
            var locator = By.XPath($"//input[@name='isActive' and @value='{status}']/following-sibling::label");
            var label = _state.Wait.WaitUntilElementToBeClickable(locator);

            ((IJavaScriptExecutor)_state.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", label);
            Thread.Sleep(300);

            label.Click();

            var readable = status.Equals("true", StringComparison.OrdinalIgnoreCase) ? "Yes" : "No";
            _state.Test.Log(Status.Info, $"Set Active Status: {readable}");
        }


        public void ClickSave()
        {
            _state.Wait.WaitUntilElementToBeClickable(_saveButton).Click();
        }

        public void ClickCancel()
        {
            _state.Wait.WaitUntilElementToBeClickable(_cancelButton).Click();
        }

        public string GetSuccessMessage() //Get success Message
        {
            try
            {
                var successMessage = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']"));
                return successMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetErrorMessage() //Get error message
        {
            try
            {
                var errorMessage = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']"));
                return errorMessage.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetTextOfTitleFieldErrorMessage()
        {
            var titleFieldErrorElement = _state.Wait.WaitUntilElementIsVisible(_titleFieldError);
            return titleFieldErrorElement.Text;
        }

        public string GetWorkSamplesErrorText()
        {
            var errorMessage = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[contains(@class,'ns-box-inner')]")).Text;
            return errorMessage;
        }

        public void ScrollToCenterOfThePage()
        {
            var shareSkillElement = _state.Wait.WaitUntilElementToBeClickable(_shareSkillTab);
            ((IJavaScriptExecutor)_state.Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'})",
                shareSkillElement);
        }

        public void ClickManageListingsLink()
        {
            var manageListingsElement = _state.Wait.WaitUntilElementToBeClickable(_manageListingsLink);
            manageListingsElement.Click();
        }

        public string GetRecentlyAddedSkillTitleFromManageListings()
        {
            ClickManageListingsLink();
            var table = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTable);
            var firstRow = table.FindElement(By.XPath("//tbody//tr//td[3]"));
            return firstRow.Text;
        }

        public void DeleteSpecificSharedSkill(string title)
        {
            ClickManageListingsLink();
            var table = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTable);
            var row = table.FindElement(By.XPath($".//tbody/tr[td[3][normalize-space(text())='{title}']]")); // find the row where 3rd column text matches the title
            var deleteButton = row.FindElement(By.XPath(".//button[i[contains(@class,'remove icon')]]")); // find the delete button in that row
            deleteButton.Click();
            var deleteYesButton = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//button[@class='ui icon positive right labeled button']"));
            deleteYesButton.Click();
        }

        public string GetRecentlyAddedSkillDescriptionFromManageListings()
        {
            ClickManageListingsLink();
            var table = _state.Wait.WaitUntilElementToBeClickable(_manageListingsTable);
            var firstRow = table.FindElement(By.XPath("//tbody//tr//td[4]"));
            return firstRow.Text;
        }

        public string GetTextOfDescriptionFieldError()
        {
            var descriptionFieldErrorElement = _state.Wait.WaitUntilElementToBeClickable(_descriptionFieldError);
            return descriptionFieldErrorElement.Text;
        }

        public void ClickViewButton()
        {
            ClickManageListingsLink();
            var viewButtonElement = _state.Wait.WaitUntilElementToBeClickable(_viewAddedSkills);
            viewButtonElement.Click();
        }

        public string GetServiceTypeText()
        {
            var serviceTypeElement = _state.Wait.WaitUntilElementIsVisible(_serviceTypeFromAddedSkills);
            return serviceTypeElement.Text;
        }

        public string GetLocationTypeText()
        {
            var locationTypeElement = _state.Wait.WaitUntilElementIsVisible(_locationTypeFromAddedSkills);
            return locationTypeElement.Text;
        }

        public void OpenEventSlot(int middleRowIndex, int columnIndex)
        {
            // middleRowIndex: 1-based index among rows that contain class 'k-middle-row'
            // columnIndex:    1-based <td> index (slot/column) inside that row

            var gridCellElement =
                $"(//div[@class='k-scheduler-content']//table[@class='k-scheduler-table']//tbody//tr[contains(@class,'k-middle-row')])[{middleRowIndex}]//td[{columnIndex}]";
            var wait = new WebDriverWait(_state.Driver, TimeSpan.FromSeconds(10));
            var gridCell = wait.Until(d => d.FindElement(By.XPath(gridCellElement)));

            ((IJavaScriptExecutor)_state.Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});",
                gridCell); // Scroll into view (center) to avoid sticky headers/overlays

            Actions actions = new Actions(_state.Driver);
            actions.MoveToElement(gridCell).DoubleClick().Perform(); // Double-click to open the event editor

            wait.Until(d =>
                d.FindElements(By.XPath("//span[@class='k-window-title']"))); // Wait for event editor to appear
        }

        public void SelectKendoDropdownOption(string containerFor, string optionText)
        {
            //  Scope to the field (Owner uses data-container-for='ownerId' and repeat uses data-container-for ='recurrenceRule')
            var field = _state.Wait.WaitUntilElementToBeClickable(By.XPath($"//div[@data-container-for='{containerFor}']")); //Repeat and Owner
            if (containerFor.Equals("recurrenceRule"))
            {
                var textBox = field.FindElement(By.XPath("//span[@title='Recurrence editor']//span[@class='k-dropdown-wrap k-state-default']"));
                textBox.Click();

                Actions actions = new Actions(_state.Driver);
                actions.MoveToElement(textBox).SendKeys(optionText).Click().Perform(); // hover action
            }
            else if (containerFor.Equals("ownerId"))
            {
                var textBox = field.FindElement(By.XPath("//span[@title='No title']"));
                textBox.Click();

                Actions actions = new Actions(_state.Driver);
                actions.MoveToElement(textBox).SendKeys(optionText).Click().Perform(); // hover action
            }
        }

        public void SaveEventDetails(string eventTitle, string startDateTime, string endDateTime, string repeatOptions,
            string description, string owner)
        {
            var eventTitleElement = _state.Wait.WaitUntilElementIsVisible(_eventTitle);
            eventTitleElement.Clear();
            eventTitleElement.SendKeys(eventTitle);

            var startEventDateTimeElement = _state.Wait.WaitUntilElementIsVisible(By.XPath("//input[@data-bind='value:start,invisible:isAllDay']"));
            startEventDateTimeElement.Clear();
            startEventDateTimeElement.SendKeys(startDateTime);

            Thread.Sleep(3000);
            var endEventDateTimeElement = _state.Wait.WaitUntilElementIsVisible(By.XPath("//input[@data-bind='value:end,invisible:isAllDay']"));
            endEventDateTimeElement.Clear();
            endEventDateTimeElement.SendKeys(endDateTime);

            var allDayEventElement = _state.Wait.WaitUntilElementIsVisible(_allDayEventCheckBox);
            allDayEventElement.Click();

            SelectKendoDropdownOption("recurrenceRule", repeatOptions);

            var eventDescription = _state.Wait.WaitUntilElementIsVisible(_descriptionTextBoxInsideEvent);
            eventDescription.SendKeys(description);

            SelectKendoDropdownOption("ownerId", owner);

            var saveButtonElement = _state.Wait.WaitUntilElementToBeClickable(_saveEventButton);
            saveButtonElement.Click();
        }

        public void CancelEventDetails(string eventTitle, string startDateTime, string endDateTime,
            string repeatOptions, string description, string owner)
        {
            var eventTitleElement = _state.Wait.WaitUntilElementIsVisible(_eventTitle);
            eventTitleElement.Clear();
            eventTitleElement.SendKeys(eventTitle);

            var startEventDateTimeElement = _state.Wait.WaitUntilElementIsVisible(By.XPath("//input[@data-bind='value:start,invisible:isAllDay']"));
            startEventDateTimeElement.Clear();
            startEventDateTimeElement.SendKeys(startDateTime);

            Thread.Sleep(3000);
            var endEventDateTimeElement = _state.Wait.WaitUntilElementIsVisible(By.XPath("//input[@data-bind='value:end,invisible:isAllDay']"));
            endEventDateTimeElement.Clear();
            endEventDateTimeElement.SendKeys(endDateTime);

            var allDayEventElement = _state.Wait.WaitUntilElementIsVisible(_allDayEventCheckBox);
            allDayEventElement.Click();

            SelectKendoDropdownOption("recurrenceRule", repeatOptions);

            var eventDescription = _state.Wait.WaitUntilElementIsVisible(_descriptionTextBoxInsideEvent);
            eventDescription.SendKeys(description);

            SelectKendoDropdownOption("ownerId", owner);

            var cancelButtonElement = _state.Wait.WaitUntilElementToBeClickable(_cancelEventButton);
            cancelButtonElement.Click();
        }

        public void LeaveEitherOneOrAllRequiredFieldsEmpty(string? title, string? description, string? category,
            string? subCategory, List<string>? tags, string? serviceType, string? locationType, string? skillTradeType,
            string? credit, List<string>? skillExchangeTags, string? active)
        {
            if (!string.IsNullOrWhiteSpace(title))
                EnterTitle(title.Trim());

            if (!string.IsNullOrWhiteSpace(description))
                EnterDescription(description.Trim());

            if (!string.IsNullOrWhiteSpace(category))
                SelectCategory(category.Trim());

            if (!string.IsNullOrWhiteSpace(subCategory))
                SelectSubCategory(subCategory.Trim());
            foreach (var tag in tags)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                    AddTag(tag.Trim());
            }

            if (!string.IsNullOrWhiteSpace(serviceType))
            {
                SelectServiceType(serviceType); //true = "Hourly basis", false = "One-off"
            }

            if (!string.IsNullOrWhiteSpace(locationType))
            {
                SelectLocationType(locationType); //true = "On-site", false = "Online"
            }

            if (!string.IsNullOrWhiteSpace(skillTradeType))
            {
                SelectSkillTradeType(skillTradeType);
            }

            foreach (var skillExchangeTag in skillExchangeTags)
            {
                if (!string.IsNullOrWhiteSpace(skillExchangeTag))
                {
                    AddSkillExchangeTag(skillExchangeTag.Trim());
                }
            }

            if (!string.IsNullOrWhiteSpace(active))
            {
                SetActiveStatus(active);
            }
        }

        public string GetTextOfFieldErrorMessageForCategory()
        {
            var categoryTextField = _state.Wait.WaitUntilElementIsVisible(_selectCategory);
            var categoryTextFieldErrorElement = categoryTextField.FindElement(_categoryFieldError);
            return categoryTextFieldErrorElement.Text;
        }

        public string GetTextOfFieldErrorMessageForTags()
        {
            var tagsTextField = _state.Wait.WaitUntilElementIsVisible(_tagsInputBox);
            var tagsTextFieldErrorElement = tagsTextField.FindElement(_tagsFieldError);
            return tagsTextFieldErrorElement.Text;
        }

        public string GetTextOfFieldErrorMessageForSkillTags()
        {
            var skillTagsTextField = _state.Wait.WaitUntilElementIsVisible(_skillExchangeTagInputBox);
            var skillTagsTextFieldErrorElement = skillTagsTextField.FindElement(_skillExchangeFieldError);
            return skillTagsTextFieldErrorElement.Text;
        }

        public void SaveEvent(Models.EventModel eventDetails)
        {
            // Title
            Thread.Sleep(3000);
            var title = _state.Wait.WaitUntilElementIsVisible(_eventTitle);
            title.Clear();
            title.SendKeys(eventDetails.EventTitle);

            // Start/End (assumes your Kendo mask matches the provided strings)
            var startDate = _state.Wait.WaitUntilElementIsVisible(By.XPath("//input[@data-bind='value:start,invisible:isAllDay']"));
            startDate.Clear();
            startDate.SendKeys(eventDetails.StartDateTime);

            var endDate = _state.Wait.WaitUntilElementIsVisible(By.XPath("//input[@data-bind='value:end,invisible:isAllDay']"));
            endDate.Clear();
            endDate.SendKeys(eventDetails.EndDateTime);

            // All-day checkbox 
            Thread.Sleep(5000);
            _state.Wait.WaitUntilElementIsVisible(_allDayEventCheckBox).Click();


            // Repeat rule
            if (!string.IsNullOrWhiteSpace(eventDetails.RepeatRule))
                SelectKendoDropdownOption("recurrenceRule", eventDetails.RepeatRule.Trim());

            switch (eventDetails.RepeatRule?.Trim())
            {
                case "Never":
                    break;

                case "Daily":
                    if (!string.IsNullOrWhiteSpace(eventDetails.RepeatDays))
                    {
                        var upArrow = _state.Wait.WaitUntilElementToBeClickable(_repeatEveryDaysUpArrow);
                        upArrow.Click();
                        var clickOutsideTheField = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//div[@name='recurrenceRule']"));
                        clickOutsideTheField.Click(); //To click outside the field to make th
                    }

                    if (!string.IsNullOrWhiteSpace(eventDetails.RepeatEndOn))
                    {
                        _state.Wait.WaitUntilElementToBeClickable(_endOnRadioButton).Click();
                        var on = _state.Wait.WaitUntilElementToBeClickable(_endOnDateTextBox);
                        on.Clear();
                        on.SendKeys(eventDetails.RepeatEndOn.Trim());
                    }

                    break;

                case "Weekly":
                    if (!string.IsNullOrWhiteSpace(eventDetails.RepeatWeeks))
                    {
                        var upArrow = _state.Wait.WaitUntilElementToBeClickable(_repeatEveryDaysUpArrow);
                        upArrow.Click();
                    }

                    if (!string.IsNullOrWhiteSpace(eventDetails.RepeatDay))
                    {
                        var checkBoxes = _state.Driver.FindElements(By.XPath("//label[@class='k-check']//input[@class='k-recur-weekday-checkbox']"));
                        foreach (var checkbox in checkBoxes)
                        {
                            if (checkbox.Selected) // Only click if currently checked
                            {
                                checkbox.Click(); // Toggle to uncheck
                            }
                        }

                        _state.Wait.WaitUntilElementToBeClickable(By.XPath($"//label[normalize-space()='{eventDetails.RepeatDay.Trim()}']")).Click();
                    }

                    if (!string.IsNullOrWhiteSpace(eventDetails.RepeatEndNever))
                        _state.Wait.WaitUntilElementToBeClickable(_endNeverRadioButton).Click();
                    break;

                case "Monthly":
                    {
                        var interval = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@class='k-formatted-value k-recur-interval k-input']"));
                        interval.Clear();
                        interval.SendKeys(!string.IsNullOrWhiteSpace(eventDetails.RepeatMonths) ? eventDetails.RepeatMonths.Trim() : "1");

                        if (!string.IsNullOrWhiteSpace(eventDetails.RepeatEvery))
                        {
                            var repeatOnEvery = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@value='weekday']"));
                            repeatOnEvery.Click();
                            var repeatOn = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//span[@title='Repeat on: ']"));
                            repeatOn.Click();

                            Actions actions = new Actions(_state.Driver);
                            actions.MoveToElement(repeatOn).SendKeys(eventDetails.RepeatEvery).Click().Perform();
                        }

                        if (!string.IsNullOrWhiteSpace(eventDetails.RepeatWeekday))
                        {
                            var day = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//span[@title='Day ']"));
                            day.Click();

                            Actions actions = new Actions(_state.Driver);
                            actions.MoveToElement(day).SendKeys(eventDetails.RepeatWeekday).Click().Perform();
                        }

                        if (!string.IsNullOrWhiteSpace(eventDetails.RepeatEndOn))
                        {
                            _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@value='until']")).Click();
                            var on = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[contains(@title,'On')]"));
                            on.Clear();
                            on.SendKeys(eventDetails.RepeatEndOn.Trim());
                        }
                    }
                    break;
                case "Yearly":
                    {
                        var interval = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@class='k-formatted-value k-recur-interval k-input']"));
                        interval.Clear();
                        interval.SendKeys(!string.IsNullOrWhiteSpace(eventDetails.RepeatYears) ? eventDetails.RepeatYears.Trim() : "1");

                        if (!string.IsNullOrWhiteSpace(eventDetails.YearlyRepeatOnWeekday))
                            _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@value='weekday']")).Click();

                        // Fill dropdowns/textboxes for "Every / Weekday / Month"
                        if (!string.IsNullOrWhiteSpace(eventDetails.YearlyEvery))
                        {
                            var repeatOn = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//span[contains(@title,'Repeat on:')]//span[@class='k-dropdown-wrap k-state-default']//span[@class='k-icon k-i-arrow-60-down']"));
                            repeatOn.Click();

                            Actions actions = new Actions(_state.Driver);
                            actions.MoveToElement(repeatOn).SendKeys(eventDetails.YearlyEvery).Click().Perform();

                        }

                        if (!string.IsNullOrWhiteSpace(eventDetails.YearlyWeekday))
                        {
                            var day = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//span[@title='']//span[@class='k-dropdown-wrap k-state-default']"));
                            day.Click();

                            Actions actions = new Actions(_state.Driver);
                            actions.MoveToElement(day).SendKeys(eventDetails.YearlyWeekday).Click().Perform();
                        }

                        if (!string.IsNullOrWhiteSpace(eventDetails.YearlyMonth))
                        {
                            var month = _state.Wait.WaitUntilElementIsVisible(By.XPath($"//span[@class='k-dropdown-wrap k-state-default']//span[@class='k-input'][normalize-space()='{eventDetails.YearlyMonth}']"));
                            month.Click();

                            Actions actions = new Actions(_state.Driver);
                            actions.MoveToElement(month).SendKeys(eventDetails.YearlyMonth).Click().Perform();
                        }

                        // End after N occurrences
                        if (!string.IsNullOrWhiteSpace(eventDetails.YearlyCount))
                        {
                            _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@value='count']")).Click();
                            var count = _state.Wait.WaitUntilElementToBeClickable(By.XPath("//input[@class='k-formatted-value k-recur-count k-input']"));
                            count.Clear();
                            count.SendKeys(eventDetails.YearlyCount.Trim());
                        }

                        break;
                    }
            }

            if (!string.IsNullOrWhiteSpace(eventDetails.Description))
                _state.Wait.WaitUntilElementIsVisible(_descriptionTextBoxInsideEvent).SendKeys(eventDetails.Description.Trim());

            if (!string.IsNullOrWhiteSpace(eventDetails.Owner))
                SelectKendoDropdownOption("ownerId", eventDetails.Owner.Trim());

            _state.Wait.WaitUntilElementToBeClickable(_saveEventButton).Click();
        }

        public string GetEventFromCalendar()
        {
            var calendarContent = _state.Wait.WaitUntilElementIsVisible(By.XPath("//div[@class='k-scheduler-content']"));
            return calendarContent.Text;
        }

        public void EditShareSkill(string title, string description, string category, string subCategory)
        {
            var enterTitleTextBox = _state.Wait.WaitUntilElementToBeClickable(_titleTextField);
            enterTitleTextBox.Clear();
            enterTitleTextBox.SendKeys(title);

            var enterDescriptionTextBox = _state.Wait.WaitUntilElementToBeClickable(_descriptionTextField);
            enterDescriptionTextBox.Clear();
            enterDescriptionTextBox.SendKeys(description);

            var selectCategory = _state.Wait.WaitUntilElementToBeClickable(_selectCategory);
            SelectElement categoryDropDown = new SelectElement(selectCategory);
            categoryDropDown.SelectByText(category);

            var selectSubCategory = _state.Wait.WaitUntilElementToBeClickable(_selectSubCategory);
            SelectElement subCategoryDropDown = new SelectElement(selectSubCategory);
            subCategoryDropDown.SelectByText(subCategory);
        }

        public void ClickEditIcon(string skillTitle)
        {
            var rows = _state.Driver.FindElements(By.XPath("//tbody/tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains(skillTitle))
                {
                    var editIcon = row.FindElement(By.XPath(".//i[contains(@class,'write icon')]"));
                    editIcon.Click();
                    break;
                }
            }
        }

        public void AddShareSkills(Models.ShareSkillDetails skill)
        {
            ScrollToCenterOfThePage();
            ClickShareSkill();
            EnterTitle(skill.Title);
            EnterDescription(skill.Description);
            SelectCategory(skill.Category);
            SelectSubCategory(skill.SubCategory);
            foreach (var tag in skill.Tags)
            {
                AddTag(tag);
            }

            SelectServiceType(skill.ServiceType);
            SelectLocationType(skill.LocationType);

            ClickCalendarAndSelectCurrentDate();
            ClickWeekLink();
            SelectSkillTradeType(skill.SkillTradeType);

            foreach (var skillTag in skill.SkillExchangeTags)
            {
                AddSkillExchangeTag(skillTag);
            }

            foreach (var workSample in skill.WorkSamples)
            {
                var fullPath = Path.GetFullPath(workSample);
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException("File not found:" + fullPath);
                }
                UploadWorkSample(fullPath);
            }
            SetActiveStatus(skill.Active);
            ClickSave();
        }

        public void UpdateShareSkills(Models.ShareSkillDetails skillToUpdate)
        {
            EnterTitle(skillToUpdate.Title);
            EnterDescription(skillToUpdate.Description);
            SelectCategory(skillToUpdate.Category);
            SelectSubCategory(skillToUpdate.SubCategory);
            foreach (var tag in skillToUpdate.Tags)
            {
                AddTag(tag);
            }

            SelectServiceType(skillToUpdate.ServiceType);
            SelectLocationType(skillToUpdate.LocationType);

            ClickCalendarAndSelectCurrentDate();
            ClickWeekLink();
            SelectSkillTradeType(skillToUpdate.SkillTradeType);

            foreach (var skillTag in skillToUpdate.SkillExchangeTags)
            {
                AddSkillExchangeTag(skillTag);
            }

            foreach (var workSample in skillToUpdate.WorkSamples)
            {
                var fullPath = Path.GetFullPath(workSample);
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException("File not found:" + fullPath);
                }
                UploadWorkSample(fullPath);
            }
            SetActiveStatus(skillToUpdate.Active);
            ClickSave();
        }
    }
}


