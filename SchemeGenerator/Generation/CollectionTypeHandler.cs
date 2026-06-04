using System.Collections;

namespace SchemeGenerator.Generation;

internal static class CollectionTypeHandler
{
    public static bool TryGetCollectionElementType(Type type, out Type elementType)
    {
        if (DictionaryTypeHandler.TryGetDictionaryTypes(type, out _, out _))
        {
            elementType = typeof(object);
            return false;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            elementType = type.GetGenericArguments()[0];
            return true;
        }

        foreach (var iface in type.GetInterfaces())
        {
            if (iface.IsGenericType &&
                iface.GetGenericTypeDefinition() == typeof(ICollection<>) &&
                iface.GetGenericArguments() is [var arg])
            {
                elementType = arg;
                return true;
            }
        }

        elementType = typeof(object);
        return false;
    }

    public static object Create(
        Type collectionType,
        Type elementType,
        int count,
        Func<Type, object?> generateElement)
    {
        IList list;
        if (collectionType.IsInterface || collectionType.IsAbstract)
        {
            list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType))!;
        }
        else
        {
            list = (IList)Activator.CreateInstance(collectionType)!;
        }

        for (var i = 0; i < count; i++)
        {
            list.Add(generateElement(elementType));
        }

        return list;
    }
}
