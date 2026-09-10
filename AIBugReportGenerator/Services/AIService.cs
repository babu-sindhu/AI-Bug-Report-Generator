using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AIBugReportGenerator.Models;

namespace AIBugReportGenerator.Services
{
    /// <summary>
    /// Simple AI service that calls an external AI API to convert tester notes into a structured BugReport.
    /// Reads the API key from the OPENAI_API_KEY environment variable and throws clear exceptions on errors.
    /// </summary>
    public class AIService
    {
        private readonly string _apiKey;

        public AIService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set. Set it to your API key before running the application.");
            }
        }

        public async Task<BugReport> GenerateAsync(string bugNotes)
        {
            bugNotes ??= string.Empty;

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var systemPrompt = "You are an assistant that converts raw tester notes into a JSON object with the following fields: BugTitle, Description, ReproductionSteps, ActualResult, ExpectedResult, Environment, Severity, Priority. Return ONLY valid JSON matching this structure. Use plain strings. ReproductionSteps may contain newline characters to separate steps. Do not return any explanatory text or surrounding markdown.";
            var userPrompt = $"Convert the following tester notes into JSON matching the specified fields. Notes:\n{bugNotes}";

            var requestObj = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                temperature = 0.0
            };

            var requestJson = JsonSerializer.Serialize(requestObj);
            using var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await http.PostAsync("https://api.openai.com/v1/chat/completions", content);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Failed to send request to AI API.", ex);
            }

            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"AI API returned non-success status code {(int)response.StatusCode}: {response.ReasonPhrase}. Response body: {responseText}");
            }

            // Extract the assistant message content from the response JSON.
            string assistantContent;
            try
            {
                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;
                if (!root.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
                {
                    throw new FormatException("AI response does not contain choices.");
                }

                var first = choices[0];
                if (!first.TryGetProperty("message", out var message) || !message.TryGetProperty("content", out var contentEl))
                {
                    throw new FormatException("AI response does not contain a message content.");
                }

                assistantContent = contentEl.GetString() ?? string.Empty;
            }
            catch (JsonException ex)
            {
                throw new FormatException("Failed to parse AI API JSON response.", ex);
            }

            if (string.IsNullOrWhiteSpace(assistantContent))
            {
                throw new FormatException("AI returned an empty response content.");
            }

            // The assistantContent should be valid JSON matching BugReport. Deserialize.
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var report = JsonSerializer.Deserialize<BugReport>(assistantContent, options);
                if (report == null)
                {
                    throw new FormatException("AI returned JSON but it could not be deserialized into a BugReport.");
                }

                return report;
            }
            catch (JsonException ex)
            {
                throw new FormatException("AI returned invalid JSON for BugReport.", ex);
            }
        }
    }
}
