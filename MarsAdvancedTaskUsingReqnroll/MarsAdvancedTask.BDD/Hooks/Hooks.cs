using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using MarsAdvancedTask.BDD.Drivers;
using MarsAdvancedTask.Framework.Helpers;
using MarsAdvancedTask.Framework.Models;
using MarsAdvancedTask.Framework.Pages;
using MarsAdvancedTask.Framework.Pages.ManageListingsComponent;
using MarsAdvancedTask.Framework.Pages.Profile_Components;
using Reqnroll;
using Reqnroll.BoDi;

namespace MarsAdvancedTask.BDD.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly TestState _state;
        private readonly IObjectContainer _objectContainer;  //BoDi container to share objects across the framework
        private static ExtentReports? _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenarioTest;
        private static Settings? _config;

        public Hooks(IObjectContainer objectContainer, TestState state)
        {
            _objectContainer = objectContainer;
            _state = state;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _config = JsonHelper.ReadJson<Settings>("settings.json");
            _extent = ExtentManager.GetExtentReports(_config);  // Singleton pattern
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            if (_extent != null) _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenarioTest = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title); //Extent node for this scenario

            // Create WebDriver using DriverFactory
            _state.Driver = DriverFactory.CreateDriver(_config.Browser);
            _state.Driver.Navigate().GoToUrl(_config.Environment.BaseUrl);
            _state.Driver.Manage().Window.Maximize();

            _state.Test = _scenarioTest; //Assign test to state early (scenario level node)

            _state.Wait = new WaitHelper(_state.Driver);  //Helpers 
            _state.ScreenshotHelper = new ScreenshotHelper(_state.Driver);
            _state.Assert = new AssertHelper(_state);
            _state.JsonHelper = new JsonHelper();
            _state.SignInComponent = new SignInComponent(_state);

            //Register with BoDi
            _objectContainer.RegisterInstanceAs(_state.Test);
            _objectContainer.RegisterInstanceAs(_state.Driver);
            _objectContainer.RegisterInstanceAs(_state.Wait);
            _objectContainer.RegisterInstanceAs(_state.Assert);
            _objectContainer.RegisterInstanceAs(_state.JsonHelper);
            _objectContainer.RegisterInstanceAs(_state.ScreenshotHelper);
            _objectContainer.RegisterInstanceAs(_state.SignInComponent);
            Console.WriteLine($"[BeforeScenario] " +
                              $"Scenario: '{scenarioContext.ScenarioInfo.Title}', " +
                              $"Thread: {Thread.CurrentThread.ManagedThreadId}, " +
                              $"State.InstanceId: {_state.InstanceId}");

        }

        [AfterStep]

        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;
            var stepText = stepInfo.Text;
            var stepType = stepInfo.StepDefinitionType.ToString();

            ExtentTest node = stepInfo.StepDefinitionType switch
            {
                Reqnroll.Bindings.StepDefinitionType.Given => _state.Test.CreateNode<Given>(stepText),
                Reqnroll.Bindings.StepDefinitionType.When => _state.Test.CreateNode<When>(stepText),
                Reqnroll.Bindings.StepDefinitionType.Then => _state.Test.CreateNode<Then>(stepText),
                _ => _state.Test.CreateNode<And>(stepText)
            };

            if (scenarioContext.TestError == null)
            {
                // No duplicate text here:
                node.Pass("Step passed");
                Console.WriteLine($"Step Passed: {stepText}");
            }
            else
            {
                string screenshotPath = _state.ScreenshotHelper.CaptureAsBase64(stepText);
                node.AddScreenCaptureFromBase64String(screenshotPath, "Failure Screenshot");
                node.Fail($"{stepType} : {stepText} - {scenarioContext.TestError.Message}");
            }
        }

        [AfterScenario]
        public void CleanUpDataAfterScenario(FeatureContext featureContext)
        {
            var tags = featureContext.FeatureInfo.Tags;
            if (tags.Contains("EducationAdd") || tags.Contains("EducationDelete"))
            {
                var educationAddComponent = new EducationAddComponent(_state);
                foreach (var education in _state.CleanupEducationAdd)
                {
                    try
                    {
                        educationAddComponent.DeleteSpecificEducation(education);
                        Console.WriteLine($"[CleanUp] Deleted education:{education}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[CLEANUP FAILED] Education: {education} — {ex.Message}");
                    }
                }
                _state.Test.Log(Status.Info, $"CleanUp Completed");
                //State.LanguagesCleanUp.Clear(); // Reset for next test
            }
            else if (tags.Contains("CertificationsAdd") || tags.Contains("CertificationsDelete"))
            {
                var certificationsAddComponent = new CertificationsAddComponent(_state);
                foreach (var certification in _state.CleanupCertificationAdd)
                {
                    try
                    {
                        certificationsAddComponent.DeleteSpecificCertification(certification);
                        Console.WriteLine($"[CleanUp] Deleted certification:{certification}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[CLEANUP FAILED] Certification: {certification} — {ex.Message}");
                    }
                }
            }
            else if (tags.Contains("ManageListingsView"))
            {
                var manageListingsEditComponent = new ManageListingsEditComponent(_state);
                foreach (var manageListing in _state.CleanupManageListings)
                {
                    try
                    {
                        manageListingsEditComponent.DeleteSpecificSharedSkill(manageListing);
                        Console.WriteLine($"[CleanUp] Deleted skill from Manage listings:{manageListing}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[CLEANUP FAILED] Manage Lsitings: {manageListing} — {ex.Message}");
                    }
                }
            }
            else if (tags.Contains("ManageListingsEdit"))
            {
                var manageListingsEditComponent = new ManageListingsEditComponent(_state);
                foreach (var manageListingEdit in _state.CleanupManageListingsEdit)
                {
                    try
                    {
                        manageListingsEditComponent.DeleteSpecificSharedSkill(manageListingEdit);
                        Console.WriteLine($"[CleanUp] Deleted skill from Manage listings:{manageListingEdit}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[CLEANUP FAILED] Manage Listings: {manageListingEdit} — {ex.Message}");
                    }
                }
            }
            _state.Driver.Quit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }
    }
}

