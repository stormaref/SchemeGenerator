namespace SchemeGenerator.Attributes;

/// <summary>
/// Supplies a fixed value for a property (primitives, string, enum, or null).
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SchemeValueAttribute(object? value) : Attribute
{
    /// <summary>
    /// Fixed value to assign.
    /// </summary>
    public object? Value { get; } = value;
}
