namespace MarsAdvancedTask.Framework.Models
{
    public class EventModel
    {
        public string EventTitle { get; set; } = string.Empty;
        public string StartDateTime { get; set; } = string.Empty;   // e.g. 08/09/2025 09:00 AM
        public string EndDateTime { get; set; } = string.Empty;
        public string AllDay { get; set; } = string.Empty;       // true/false or "" to skip

        // Repeat
        public string RepeatRule { get; set; } = "Never"; // Never, Daily, Weekly, Monthly, Yearly

        // Daily 
        public string RepeatDays { get; set; } = string.Empty;      // 1, 2...

        // Weekly
        public string RepeatWeeks { get; set; } = string.Empty;     // 1
        public string RepeatDay { get; set; } = string.Empty;       // Monday

        //Monthly
        public string RepeatMonths { get; set; } = string.Empty;    // 1
        public string RepeatEvery { get; set; } = string.Empty;     // First, Second... 
        public string RepeatWeekday { get; set; } = string.Empty;   // Monday

        //Yearly 
        public string RepeatYears { get; set; } = string.Empty;
        public string YearlyRepeatOnWeekday { get; set; } = string.Empty; // true/false
        public string YearlyEvery { get; set; } = string.Empty;
        public string YearlyWeekday { get; set; } = string.Empty;
        public string YearlyMonth { get; set; } = string.Empty;
        public string YearlyCount { get; set; } = string.Empty;

        public string RepeatEndNever { get; set; } = string.Empty;  // true/false
        public string RepeatEndAfter { get; set; } = string.Empty;  // number of occurrences
        public string RepeatEndOn { get; set; } = string.Empty;     // end date, e.g. 31/12/2025

        public string Description { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string ExpectedMessageForEvent { get; set; } = string.Empty;
    }
}
