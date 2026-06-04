using System.Collections;

namespace SchemeGenerator.Generation;

internal static class DictionaryTypeHandler
{
    public static bool TryGetDictionaryTypes(Type type, out Type keyType, out Type valueType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        {
            keyType = type.GetGenericArguments()[0];
            valueType = type.GetGenericArguments()[1];
            return true;
        }

        foreach (var iface in type.GetInterfaces())
        {
            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                keyType = iface.GetGenericArguments()[0];
                valueType = iface.GetGenericArguments()[1];
                return true;
            }
        }

        keyType = typeof(object);
        valueType = typeof(object);
        return false;
    }

    public static object Create(
        Type dictionaryType,
        Type keyType,
        Type valueType,
        Func<Type, object?> generateValue)
    {
        var instance = Activator.CreateInstance(
            typeof(Dictionary<,>).MakeGenericType(keyType, valueType))!;
        var dict = (IDictionary)instance;
        var key = generateValue(keyType) ?? Activator.CreateInstance(keyType)!;
        dict.Add(key, generateValue(valueType));
        return instance;
    }
}
