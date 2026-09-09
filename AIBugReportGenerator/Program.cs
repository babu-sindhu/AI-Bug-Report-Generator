using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIBugReportGenerator.Services;
using AIBugReportGenerator.Models;

// Simple, beginner-friendly console flow for collecting bug notes and displaying a generated report.
Console.WriteLine("AI Bug Report Generator");
Console.WriteLine();
Console.WriteLine("Enter your bug notes. Type END on a new line when finished:");

var lines = new List<string>();
while (true)
{
    var line = Console.ReadLine();
    if (line == null)
    {
        // End input if stream is closed
        break;
    }

    if (line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    lines.Add(line);
}

var bugNotes = string.Join("\n", lines);

var service = new BugReportGeneratorService();
var report = await service.GenerateAsync(bugNotes);

// Display the generated bug report sections
Console.WriteLine();
Console.WriteLine("--- Generated Bug Report ---");
Console.WriteLine();
Console.WriteLine($"Bug Title: {report.BugTitle}");
Console.WriteLine();
Console.WriteLine("Description:");
Console.WriteLine(report.Description);
Console.WriteLine();
Console.WriteLine("Reproduction Steps:");
Console.WriteLine(report.ReproductionSteps);
Console.WriteLine();
Console.WriteLine("Actual Result:");
Console.WriteLine(report.ActualResult);
Console.WriteLine();
Console.WriteLine("Expected Result:");
Console.WriteLine(report.ExpectedResult);
Console.WriteLine();
Console.WriteLine("Environment:");
Console.WriteLine(report.Environment);
Console.WriteLine();
Console.WriteLine($"Severity: {report.Severity}");
Console.WriteLine($"Priority: {report.Priority}");

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
