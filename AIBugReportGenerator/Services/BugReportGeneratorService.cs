using System.Threading.Tasks;
using AIBugReportGenerator.Models;

namespace AIBugReportGenerator.Services
{
    /// <summary>
    /// Service responsible for generating bug reports. Delegates to the AIService to perform AI-based generation.
    /// </summary>
    public class BugReportGeneratorService
    {
        public Task<BugReport> GenerateAsync(string prompt)
        {
            var ai = new AIService();
            return ai.GenerateAsync(prompt ?? string.Empty);
        }
    }
}
