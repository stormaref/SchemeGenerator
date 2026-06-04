using System.Reflection;
using Xunit.Sdk;

namespace SchemeGenerator.Xunit;

/// <summary>
/// Supplies a generated default value of <typeparamref name="T"/> as xUnit theory data.
/// </summary>
public sealed class AutoSchemeDataAttribute<T> : DataAttribute
{
    private readonly ISchemeGenerator _generator = new global::SchemeGenerator.SchemeGenerator();

    /// <inheritdoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return [_generator.GetDefault<T>()!];
    }
}
