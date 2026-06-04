namespace SchemeGenerator;

/// <summary>
/// Generates default object graphs and JSON for .NET types.
/// </summary>
public interface ISchemeGenerator
{
    /// <summary>
    /// Gets a default value for the specified type.
    /// </summary>
    object? GetDefaultValue(Type type);

    /// <summary>
    /// Gets a default value for <typeparamref name="T"/>.
    /// </summary>
    T? GetDefault<T>();

    /// <summary>
    /// Serializes a default value for the type as JSON.
    /// </summary>
    string GetDefaultJson(Type type);

    /// <summary>
    /// Serializes a default value for <typeparamref name="T"/> as JSON.
    /// </summary>
    string GetDefaultJson<T>();

    /// <summary>
    /// Populates writable properties on an existing instance.
    /// </summary>
    void Populate(object instance);
}
