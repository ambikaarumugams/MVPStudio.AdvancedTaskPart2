using AventStack.ExtentReports;
using MarsAdvancedTask.Framework.Pages;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Helpers
{
    public class TestState
    {
        public IWebDriver? Driver { get; set; }
        public WaitHelper? Wait { get; set; }
        public AssertHelper? Assert { get; set; }
        public ScreenshotHelper? ScreenshotHelper { get; set; }
        public JsonHelper? JsonHelper { get; set; }
        public ExtentTest? Test { get; set; }
        public ExtentTest? StepNode { get; set; }
        public SignInComponent? SignInComponent { get; set; }

        //Education Add
        public List<string> ActualEducationMessages { get; set; } = new();  // for assertions
        public List<string> ExpectedEducationMessages { get; set; } = new();
        public List<string> CleanupEducationAdd { get; set; } = new();    // for cleanup
        public List<(string MessageType, string MessageText)> EducationTuplesMessages { get; set; } = new();
        public List<bool> ActualEducationFlags { get; set; } = new();

        // Certifications Add
        public List<string> ActualCertificationMessages { get; set; } = new();
        public List<string> ExpectedCertificationMessages { get; set; } = new();
        public List<string> CleanupCertificationAdd { get; set; } = new();
        public List<(string MessageType, string MessageText)> CertificationTuplesMessages { get; set; } = new(); // toast/type messages
        public List<bool> ActualCertificationFlags { get; set; } = new();

        //Change Password
        public List<string> ActualChangePasswordMessage { get; set; } = new();
        public List<string> ExpectedChangePasswordMessage { get; set; } = new();

        //Description
        public List<string> ActualDescriptionMessages { get; set; } = new();
        public List<string> ExpectedDescriptionMessages { get; set; } = new();
        public List<(string MessageType, string MessageText)> ActualDescriptionTupleMessages { get; set; } = new();

        //Manage Listings view
        public List<string> ActualManageListingsView { get; set; } = new();
        public List<string> ExpectedManageListingsView { get; set; } = new();
        public List<string> CleanupManageListings { get; set; } = new();

        //Manage Listings Edit Component
        public List<string> ActualManageListingsEdit { get; set; } = new();
        public List<string> ExpectedManageListingsEdit { get; set; } = new();
        public List<string> CleanupManageListingsEdit { get; set; } = new();
        public List<string> ActualFieldMessagesForManageListingsEdit { get; set; } = new();
        public List<string> ExpectedFieldMessagesForManageListingsEdit { get; set; } = new();

        //Manage Listings Delete 
        public List<string> ActualManageListingsTitleToDelete { get; set; } = new();
        public List<string> ActualManageListingsTitleFromTable { get; set; } = new();
        public List<string> ExpectedManageListingsTitleFromTable { get; set; } = new();
        public List<string> ActualManageListingsDelete { get; set; } = new();
        public List<string> ExpectedManageListingsDelete { get; set; } = new();

        //Manage Listings Send Request
        public List<string> ActualMessageForSendRequest { get; set; } = new();
        public List<string> ExpectedMessageForSendRequest { get; set; } = new();
        public List<string> CleanupForSendRequest { get; set; } = new();

        //Manage Listings Toggle On Off
        public List<string> ActualMessageForToggle { get; set; } = new();
        public List<string> ExpectedMessageForToggle { get; set; } = new();
        public string ListingSearchByCategory { get; set; } = string.Empty;

        public List<string> CleanupToggle { get; set; } = new();

        //Manage Requests Sent Request
        public List<string> ActualSentRequest { get; set; } = new();
        public List<string> CleanupSentRequest { get; set; } = new();

        //Manage Requests Complete Request
        public List<string> ActualCompleteRequest { get; set; } = new();

        public Guid InstanceId { get; } = Guid.NewGuid();  //To check each test method has own test state for parallel safe
    }
}
