namespace MarsAdvancedTask.Framework.Models
{
    public sealed class ManageListingsSendRequestModel
    {
        public SearchCriteria Search { get; set; } = new();
        public TradeRequestDetails Request { get; set; } = new();
        public ExpectedMessages Expected { get; set; } = new();
    }

    public sealed class SearchCriteria
    {
        public string Category { get; set; } = "";
        public string ListingTitle { get; set; } = "";
    }

    public sealed class TradeRequestDetails
    {
        public string Message { get; set; } = "";
    }

    public sealed class ExpectedMessages
    {
        public string SuccessMessage { get; set; } = "";
    }
}
