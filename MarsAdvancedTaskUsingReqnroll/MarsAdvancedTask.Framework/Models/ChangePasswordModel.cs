namespace MarsAdvancedTask.Framework.Models
{
    public class ChangePasswordModel
    {
        public List<ChangePasswordDetails> TestItems { get; set; } = new();
    }

    public class ChangePasswordDetails
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? ExpectedMessage { get; set; }
    }
}

