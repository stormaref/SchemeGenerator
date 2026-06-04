namespace SchemeGenerator.Generation;

internal static class PrimitiveValueFactory
{
    public static object? Create(Type type, SchemeGeneratorOptions options)
    {
        if (type == typeof(string))
        {
            return options.PrimitiveMode == PrimitiveDefaultsMode.Sample ? "sample" : string.Empty;
        }

        if (type == typeof(Guid))
        {
            return options.PrimitiveMode == PrimitiveDefaultsMode.Sample ? Guid.NewGuid() : Guid.Empty;
        }

        if (type == typeof(DateTime))
        {
            return options.PrimitiveMode == PrimitiveDefaultsMode.Sample ? DateTime.UtcNow : default(DateTime);
        }

        if (type == typeof(DateTimeOffset))
        {
            return options.PrimitiveMode == PrimitiveDefaultsMode.Sample ? DateTimeOffset.UtcNow : default(DateTimeOffset);
        }

        if (type == typeof(DateOnly))
        {
            return options.PrimitiveMode == PrimitiveDefaultsMode.Sample
                ? DateOnly.FromDateTime(DateTime.UtcNow)
                : default(DateOnly);
        }

        if (type == typeof(TimeOnly))
        {
            return options.PrimitiveMode == PrimitiveDefaultsMode.Sample ? TimeOnly.MinValue : default(TimeOnly);
        }

        if (type.IsPrimitive || type == typeof(decimal))
        {
            return Activator.CreateInstance(type);
        }

        return null;
    }

    public static bool IsPrimitiveType(Type type) =>
        type.IsPrimitive ||
        type == typeof(string) ||
        type == typeof(decimal) ||
        type == typeof(Guid) ||
        type == typeof(DateTime) ||
        type == typeof(DateTimeOffset) ||
        type == typeof(DateOnly) ||
        type == typeof(TimeOnly);
}
