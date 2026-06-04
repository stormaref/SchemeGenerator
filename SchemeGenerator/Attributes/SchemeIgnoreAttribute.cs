namespace SchemeGenerator.Attributes;

/// <summary>
/// Skips a property during scheme generation and populate.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SchemeIgnoreAttribute : Attribute;
