using System.Threading.Tasks;
using AIBugReportGenerator.Models;

namespace AIBugReportGenerator.Services
{
    /// <summary>
    /// Placeholder service responsible for generating bug reports using AI.
    /// AI integration is not implemented yet; this returns a simple template report.
    /// </summary>
    public class BugReportGeneratorService
    {
        /// <summary>
        /// Generates a bug report from a prompt. Currently returns a simple placeholder report.
        /// </summary>
        public Task<BugReport> GenerateAsync(string prompt)
        {
            // In the future this method will call an AI API to generate structured output.
            var report = new BugReport
            {
                BugTitle = "Placeholder: AI integration pending",
                Description = $"This is a placeholder report generated from the prompt: {prompt}",
                ReproductionSteps = "1. Open the application\n2. Do the action that triggers the bug",
                ActualResult = "Application crashes or shows an error",
                ExpectedResult = "Application continues normally",
                Environment = "OS: unknown; App version: unknown",
                Severity = "Medium",
                Priority = "Normal"
            };

            return Task.FromResult(report);
        }
    }
}
