# Scheme Generator

[![NuGet](https://img.shields.io/nuget/v/SchemeGenerator.svg)](https://www.nuget.org/packages/SchemeGenerator)
[![CI](https://github.com/stormaref/SchemeGenerator/actions/workflows/nuget.yml/badge.svg)](https://github.com/stormaref/SchemeGenerator/actions/workflows/nuget.yml)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-blue)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Scheme Generator builds default object graphs and JSON for any .NET type. Use it for unit tests, OpenAPI examples, and quick fixtures without hand-writing sample data.

## Packages

| Package | Description |
|---------|-------------|
| [SchemeGenerator](https://www.nuget.org/packages/SchemeGenerator) | Core reflection-based generator |
| [SchemeGenerator.SourceGenerators](https://www.nuget.org/packages/SchemeGenerator.SourceGenerators) | Roslyn source generator for compile-time defaults |
| [SchemeGenerator.Xunit](https://www.nuget.org/packages/SchemeGenerator.Xunit) | xUnit `[Theory]` data attributes |
| [SchemeGenerator.AspNetCore](https://www.nuget.org/packages/SchemeGenerator.AspNetCore) | Swashbuckle schema example filter |

All packages are version **10.1.0** and target **net8.0**, **net9.0**, and **net10.0** (except the analyzer, which is a Roslyn component).

## Requirements

- **Consumers**: .NET 8, 9, or 10
- **Building from source**: [.NET 10 SDK](https://dotnet.microsoft.com/download) ([`global.json`](global.json))

## Installation

Core package:

```bash
dotnet add package SchemeGenerator --version 10.1.0
```

With source generator:

```bash
dotnet add package SchemeGenerator --version 10.1.0
dotnet add package SchemeGenerator.SourceGenerators --version 10.1.0
```

## Quick start

```csharp
var generator = new SchemeGenerator();
var json = generator.GetDefaultJson(typeof(ComplexType));

// or with generics
var model = generator.GetDefault<ComplexType>();
var json2 = generator.GetDefaultJson<ComplexType>();
```

### Dependency injection

```csharp
builder.Services.AddSchemeGenerator(options =>
{
    options.CollectionItemCount = 2;
    options.PrimitiveMode = PrimitiveDefaultsMode.Sample;
});

// inject ISchemeGenerator
```

## Options

| Property | Default | Description |
|----------|---------|-------------|
| `CollectionItemCount` | `1` | Items in arrays and collections |
| `MaxDepth` | `32` | Max recursion depth (cycle safety) |
| `PrimitiveMode` | `Zero` | `Zero` = CLR defaults; `Sample` = realistic primitives |
| `JsonSerializerOptions` | `null` | Passed to `GetDefaultJson` |

## Attributes

| Attribute | Target | Description |
|-----------|--------|-------------|
| `[SchemeIgnore]` | Property | Skip property |
| `[SchemeCount(n)]` | Property | Override collection size |
| `[SchemeValue(x)]` | Property | Fixed value |
| `[GenerateSchemeDefaults]` | Type | Enable source generator (type must be `partial`) |

## Populate existing instances

```csharp
var target = new SimpleType();
generator.Populate(target);
```

## Source generator

Mark a **partial** type and reference the analyzer package:

```csharp
[GenerateSchemeDefaults]
public partial class OrderDto
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
}

var order = OrderDto.OrderDtoSchemeDefaults.Create();
var json = OrderDto.OrderDtoSchemeDefaults.CreateJson();
```

Nested user types without `[GenerateSchemeDefaults]` fall back to the reflection-based `SchemeGenerator` at runtime.

## xUnit integration

```bash
dotnet add package SchemeGenerator.Xunit --version 10.1.0
```

```csharp
[Theory, AutoSchemeData<ComplexType>]
public void MyTest(ComplexType model) => Assert.NotNull(model);

[Theory, SchemeGeneratorData(typeof(SimpleType))]
public void OtherTest(SimpleType model) => Assert.NotNull(model);
```

## ASP.NET Core / Swashbuckle

```bash
dotnet add package SchemeGenerator.AspNetCore --version 10.1.0
```

```csharp
builder.Services.AddSchemeGenerator();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSchemeGeneratorExamples();
});
```

Register `SchemeGeneratorSchemaFilter` via DI (Swashbuckle resolves it from the container when using `AddSchemeGeneratorExamples`).

## Example output

```csharp
public class SimpleType
{
    public int Number { get; set; }
    public string Text { get; set; }
}

public class ComplexType
{
    public List<int> Numbers { get; set; }
    public List<SimpleType> Objects { get; set; }
}
```

Default JSON (`PrimitiveMode.Zero`):

```json
{
  "Numbers": [0],
  "Objects": [{ "Number": 0, "Text": "" }]
}
```

## Benchmarks

See [SchemeGenerator.Benchmarks](SchemeGenerator.Benchmarks/):

```bash
dotnet run -c Release --project SchemeGenerator.Benchmarks
```

Compares reflection vs source-generated creation for the same DTO.

## Building locally

```bash
dotnet restore
dotnet test -c Release
dotnet pack SchemeGenerator/SchemeGenerator.csproj -c Release
dotnet pack SchemeGenerator.SourceGenerators/SchemeGenerator.SourceGenerators.csproj -c Release
dotnet pack SchemeGenerator.Xunit/SchemeGenerator.Xunit.csproj -c Release
dotnet pack SchemeGenerator.AspNetCore/SchemeGenerator.AspNetCore.csproj -c Release
```

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Release notes: [CHANGELOG.md](CHANGELOG.md).

## License

MIT — see [LICENSE](LICENSE).
