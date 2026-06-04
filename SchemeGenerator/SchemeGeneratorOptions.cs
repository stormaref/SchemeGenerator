using System.Text.Json;

namespace SchemeGenerator;

/// <summary>
/// Configures scheme generation behavior.
/// </summary>
public sealed class SchemeGeneratorOptions
{
    /// <summary>
    /// Number of items to create for collections and arrays. Default is 1.
    /// </summary>
    public int CollectionItemCount { get; set; } = 1;

    /// <summary>
    /// Maximum recursion depth when building object graphs. Default is 32.
    /// </summary>
    public int MaxDepth { get; set; } = 32;

    /// <summary>
    /// How primitives are populated. Default is <see cref="PrimitiveDefaultsMode.Zero"/>.
    /// </summary>
    public PrimitiveDefaultsMode PrimitiveMode { get; set; } = PrimitiveDefaultsMode.Zero;

    /// <summary>
    /// Optional JSON serializer options for <see cref="ISchemeGenerator.GetDefaultJson(Type)"/>.
    /// </summary>
    public JsonSerializerOptions? JsonSerializerOptions { get; set; }
}
