using System.Reflection;
using SchemeGenerator.Attributes;

namespace SchemeGenerator.Generation;

internal readonly record struct PropertyGenerationInfo(
    PropertyInfo Property,
    bool Ignored,
    int? CountOverride,
    object? FixedValue)
{
    public static PropertyGenerationInfo From(PropertyInfo property)
    {
        var ignored = property.GetCustomAttribute<SchemeIgnoreAttribute>() is not null;
        var count = property.GetCustomAttribute<SchemeCountAttribute>()?.Count;
        var fixedValue = property.GetCustomAttribute<SchemeValueAttribute>()?.Value;
        return new PropertyGenerationInfo(property, ignored, count, fixedValue);
    }
}
