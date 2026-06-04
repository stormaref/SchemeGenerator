namespace SchemeGenerator.Generation;

internal static class ArrayTypeHandler
{
    public static bool IsArray(Type type) => type.IsArray;

    public static Array Create(
        Type arrayType,
        int count,
        Func<Type, object?> generateElement)
    {
        var elementType = arrayType.GetElementType()!;
        var array = Array.CreateInstance(elementType, count);
        for (var i = 0; i < count; i++)
        {
            array.SetValue(generateElement(elementType), i);
        }

        return array;
    }
}
