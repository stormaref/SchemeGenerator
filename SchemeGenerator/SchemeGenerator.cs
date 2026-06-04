using System.Text.Json;
using SchemeGenerator.Generation;

namespace SchemeGenerator;

/// <summary>
/// Generates default object graphs and JSON using reflection.
/// </summary>
public sealed class SchemeGenerator : ISchemeGenerator
{
    private readonly ValueGenerator _generator;
    private readonly SchemeGeneratorOptions _options;

    /// <summary>
    /// Creates a generator with optional configuration.
    /// </summary>
    public SchemeGenerator(SchemeGeneratorOptions? options = null)
    {
        _options = options ?? new SchemeGeneratorOptions();
        _generator = new ValueGenerator(_options);
    }

    /// <inheritdoc />
    public object? GetDefaultValue(Type type) => _generator.Generate(type);

    /// <inheritdoc />
    public T? GetDefault<T>() => (T?)GetDefaultValue(typeof(T));

    /// <inheritdoc />
    public string GetDefaultJson(Type type)
    {
        var obj = GetDefaultValue(type);
        return JsonSerializer.Serialize(obj ?? new { }, _options.JsonSerializerOptions);
    }

    /// <inheritdoc />
    public string GetDefaultJson<T>() => GetDefaultJson(typeof(T));

    /// <inheritdoc />
    public void Populate(object instance) => _generator.Populate(instance);
}
