namespace SchemeGenerator.Attributes;

/// <summary>
/// Overrides the collection item count for a specific property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SchemeCountAttribute(int count) : Attribute
{
    /// <summary>
    /// Number of collection items to generate.
    /// </summary>
    public int Count { get; } = count;
}
