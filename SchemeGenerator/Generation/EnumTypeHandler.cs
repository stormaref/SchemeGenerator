namespace SchemeGenerator.Generation;

internal static class EnumTypeHandler
{
    public static bool IsEnumType(Type type) => type.IsEnum;

    public static object Create(Type enumType, SchemeGeneratorOptions options)
    {
        var values = Enum.GetValues(enumType);
        if (values.Length == 0)
        {
            return Activator.CreateInstance(enumType)!;
        }

        if (options.PrimitiveMode == PrimitiveDefaultsMode.Sample)
        {
            return values.GetValue(0)!;
        }

        var underlying = Enum.GetUnderlyingType(enumType);
        return Convert.ChangeType(values.GetValue(0)!, underlying);
    }
}
