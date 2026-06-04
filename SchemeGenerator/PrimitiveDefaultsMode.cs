namespace SchemeGenerator;

/// <summary>
/// Controls how primitive values are generated.
/// </summary>
public enum PrimitiveDefaultsMode
{
    /// <summary>
    /// Use CLR defaults (0, false, empty string, etc.).
    /// </summary>
    Zero,

    /// <summary>
    /// Use sample-like values (non-empty strings, current timestamps, new GUIDs).
    /// </summary>
    Sample
}
