namespace SchemeGenerator.Generation;

internal static class NullableTypeHandler
{
    public static bool IsNullableType(Type type, out Type? underlyingType)
    {
        underlyingType = Nullable.GetUnderlyingType(type);
        return underlyingType is not null;
    }
}
