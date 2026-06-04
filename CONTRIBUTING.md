# Contributing to SchemeGenerator

Thank you for your interest in contributing.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (see `global.json`)
- Familiarity with C#, reflection, and optionally Roslyn source generators

## Build and test

```bash
dotnet restore
dotnet build -c Release
dotnet test -c Release
```

## Pack locally

```bash
dotnet pack SchemeGenerator/SchemeGenerator.csproj -c Release
dotnet pack SchemeGenerator.SourceGenerators/SchemeGenerator.SourceGenerators.csproj -c Release
dotnet pack SchemeGenerator.Xunit/SchemeGenerator.Xunit.csproj -c Release
dotnet pack SchemeGenerator.AspNetCore/SchemeGenerator.AspNetCore.csproj -c Release
```

## Pull requests

1. Fork the repository and create a feature branch from `main`.
2. Add or update tests for behavior changes.
3. Update [CHANGELOG.md](CHANGELOG.md) under **Unreleased** or the target version section.
4. Ensure `dotnet test -c Release` passes with no warnings (warnings are treated as errors).
5. Open a PR with a clear description and link any related issues.

## Code style

- Match existing naming and file layout.
- Keep public API changes backward compatible within minor releases unless discussed in an issue.
- Document new public APIs with XML comments.

## Reporting issues

Use the GitHub issue templates for bugs and feature requests.
