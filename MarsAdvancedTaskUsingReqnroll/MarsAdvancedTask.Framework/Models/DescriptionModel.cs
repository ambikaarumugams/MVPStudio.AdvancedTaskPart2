namespace MarsAdvancedTask.Framework.Models
{
    public class DescriptionModel
    {
        public List<DescriptionDetails> DescriptionItems { get; set; } = new();
    }

    public class DescriptionDetails
    {
        public string DescriptionText { get; set; } = string.Empty;
        public string ExpectedDescriptionMessage {  get; set; } = string.Empty;
    }
}
