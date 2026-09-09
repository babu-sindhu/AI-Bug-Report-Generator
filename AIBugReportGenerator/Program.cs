using System;
using System.Text.Json;
using System.Threading.Tasks;
using AIBugReportGenerator.Services;

// Simple console entry point that demonstrates the placeholder generator.
Console.WriteLine("AIBugReportGenerator - starting (AI integration not implemented yet)");

var service = new BugReportGeneratorService();
var prompt = "Example: application crashes when opening settings";
var report = await service.GenerateAsync(prompt);

var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(report, options));

Console.WriteLine("Done.");
