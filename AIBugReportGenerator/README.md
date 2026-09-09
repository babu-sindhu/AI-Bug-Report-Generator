# AIBugReportGenerator

Simple beginner-friendly .NET 8.0 console application that will generate structured bug reports using AI.

This repository contains a minimal project skeleton. AI integration is intentionally not implemented yet.

Structure

AIBugReportGenerator/
├── AIBugReportGenerator.csproj
├── Program.cs
├── Models/
│   └── BugReport.cs
├── Services/
│   └── BugReportGeneratorService.cs
└── Prompts/
	└── BugReportPrompt.cs

How to build

1. Ensure you have .NET 8.0 SDK installed.
2. From the repository root run:

   dotnet build AIBugReportGenerator

Notes

- No external NuGet packages are required for this starter project.
- AI API integration will be added later in a dedicated service; current code uses placeholders.
