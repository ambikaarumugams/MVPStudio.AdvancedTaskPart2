namespace MarsAdvancedTask.Framework.Models
{
    public class EducationModel
    {
        public TestData[] TestItems { get; set; } = [];
    }

    public class TestData
    {
        public EducationDetails EducationDetailsToAdd { get; set; } = new();
        public EducationDetails? EducationDetailsToDelete { get; set; }
    }

    public class EducationDetails
    {
        public string CollegeUniversityName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string YearOfGraduation { get; set; } = string.Empty;
        public string? EducationExpectedMessage { get; set; }
    }
}
