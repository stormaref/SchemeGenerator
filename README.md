# Scheme Generator

Scheme Generator is a C# library that generates default values for a given `Type`. It also provides functionality to serialize these default values to JSON.

## Key Features

- **Default Value Generation**: Scheme Generator can generate default values for any given `Type`, including nested types and collections. This makes it easy to quickly initialize objects with default values.

- **Collection Initialization**: Not only can Scheme Generator handle collections, but it also initializes them with a single item. This is particularly useful when you need a non-empty collection for testing or other purposes.

- **JSON Serialization**: Scheme Generator can serialize the generated default values to JSON. This is handy when you need a JSON representation of an object for testing, debugging, or other uses.

## Installation

You can install the Scheme Generator package using either the `dotnet` CLI:

```bash
dotnet add package SchemeGenerator
```

Or the NuGet Package Manager Console:

```bash
Install-Package SchemeGenerator
```

## Usage

### Creating a new instance

You can create a new instance of the `SchemeGenerator` class to generate default values:

```csharp
var generator = new SchemeGenerator();
var defaultString = generator.GetDefaultJson(typeof(ComplexType));
```

### Using Dependency Injection (DI)

Alternatively, you can get an instance of `SchemeGenerator` from the DI container. First, add the Scheme Generator to your services in `Program.cs`:

```csharp
builder.Services.AddSchemeGenerator();
```

Then, inject `ISchemeGenerator` into your class:

```csharp
public class MyClass
{
    private readonly ISchemeGenerator _generator;

    public MyClass(ISchemeGenerator generator)
    {
        _generator = generator;
    }

    public void MyMethod()
    {
        var defaultString = _generator.GetDefaultJson(typeof(ComplexType));
    }
}
```

## Example

Consider the following types:
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
You can generate a default JSON representation of `ComplexType` as follows:
```csharp
var generator = new SchemeGenerator();
var json = generator.GetDefaultJson(typeof(ComplexType));
```        
This will output:
```json
{
  "Numbers": [
    0
  ],
  "Objects": [
    {
      "Number": 0,
      "Text": ""
    }
  ]
}
```