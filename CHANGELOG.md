# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [10.1.0] - 2026-06-04

### Added

- `SchemeGeneratorOptions` with collection count, max depth, primitive mode, and JSON options
- Generic APIs: `GetDefault<T>()`, `GetDefaultJson<T>()`
- `Populate(object)` to fill existing instances
- Support for enums, arrays, dictionaries, nullable types, and additional collection shapes
- Attributes: `SchemeIgnore`, `SchemeCount`, `SchemeValue`, `GenerateSchemeDefaults`
- Cycle and depth-safe generation via internal context
- `PrimitiveDefaultsMode.Sample` for realistic primitive defaults (opt-in)
- **SchemeGenerator.SourceGenerators** package with `[GenerateSchemeDefaults]` source generator
- **SchemeGenerator.Xunit** package with `SchemeGeneratorData` and `AutoSchemeData<T>` attributes
- **SchemeGenerator.AspNetCore** package with Swashbuckle `SchemeGeneratorSchemaFilter`
- **SchemeGenerator.Benchmarks** project comparing reflection vs source-generated paths
- Expanded test coverage, CONTRIBUTING guide, GitHub issue/PR templates, Dependabot

### Changed

- `AddSchemeGenerator` accepts optional `Action<SchemeGeneratorOptions>` configuration
- Centralized package version and NuGet metadata in `Directory.Build.props`
- README restructured for multi-package consumption

### Fixed

- N/A

## [10.0.0] - 2026-06-04

### Added

- .NET 10 upgrade with multi-targeting `net8.0`, `net9.0`, `net10.0`
- C# 14 extension-based DI registration
- Central package management and SDK pinning
