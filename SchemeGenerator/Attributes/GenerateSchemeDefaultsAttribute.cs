namespace SchemeGenerator.Attributes;

/// <summary>
/// Marks a type for compile-time scheme generation via SchemeGenerator.SourceGenerators.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateSchemeDefaultsAttribute : Attribute;
