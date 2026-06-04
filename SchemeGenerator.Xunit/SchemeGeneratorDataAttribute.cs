using System.Reflection;
using Xunit.Sdk;

namespace SchemeGenerator.Xunit;

/// <summary>
/// Supplies a generated default value as xUnit theory data.
/// </summary>
public sealed class SchemeGeneratorDataAttribute : DataAttribute
{
    private readonly Type _type;
    private readonly ISchemeGenerator _generator;

    /// <summary>
    /// Creates data for the given type using a new <see cref="SchemeGenerator"/>.
    /// </summary>
    public SchemeGeneratorDataAttribute(Type type)
    {
        _type = type;
        _generator = new global::SchemeGenerator.SchemeGenerator();
    }

    /// <inheritdoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return [_generator.GetDefaultValue(_type)!];
    }
}
