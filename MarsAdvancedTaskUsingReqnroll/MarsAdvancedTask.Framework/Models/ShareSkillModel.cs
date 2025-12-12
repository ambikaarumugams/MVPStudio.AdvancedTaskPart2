namespace MarsAdvancedTask.Framework.Models
{
    public class ShareSkillModel
    {
        public List<ShareSkillDetails> ShareSkills { get; set; } = new();
        public List<ShareSkillDetails> EditShareSkills { get; set; } = new();
    }

    public class ShareSkillDetails
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Category { get; set; }
        public string? SubCategory { get; set; }

        public List<string>? Tags { get; set; }

        public string? ServiceType { get; set; }
        public string? LocationType { get; set; }

        public List<CalendarEvent>? Events { get; set; } = new();

        public string? SkillTradeType { get; set; }
        public List<string>? SkillExchangeTags { get; set; }
        public string? Credit { get; set; }

        public List<string>? WorkSamples { get; set; }

        public string? Active { get; set; }

        public string? ExpectedToastMessage { get; set; }
        public string? ExpectedFieldErrorMessage { get; set; }
    }

    public class CalendarEvent
    {
        public string? Title { get; set; }
        public string? StartDateTime { get; set; }
        public string? EndDateTime { get; set; }
        public string? Repeat { get; set; }
        public string? Description { get; set; }
        public string? Owner { get; set; }
    }
}
