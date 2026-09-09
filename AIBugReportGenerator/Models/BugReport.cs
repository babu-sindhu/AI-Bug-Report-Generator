namespace AIBugReportGenerator.Models
{
    /// <summary>
    /// Represents a structured bug report.
    /// Fields are simple strings for beginner-friendly use and future extension.
    /// </summary>
    public class BugReport
    {
        public string BugTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReproductionSteps { get; set; } = string.Empty;
        public string ActualResult { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
    }
}
