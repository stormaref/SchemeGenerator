using System.Text.Json;

namespace SchemeGenerator;

public interface ISchemeGenerator
{
    string GetDefaultJson(Type type);
    object? GetDefaultValue(Type type);
}

public class SchemeGenerator : ISchemeGenerator
{
    private object InitializeClass(Type type)
    {
        var obj = Activator.CreateInstance(type);
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
            if (!property.CanWrite) continue;
            var value = GetDefaultValue(property.PropertyType);
            property.SetValue(obj, value);
        }

        return obj!;
    }

    public string GetDefaultJson(Type type)
    {
        var obj = GetDefaultValue(type);
        return JsonSerializer.Serialize(obj ?? new { });
    }

    public object? GetDefaultValue(Type type)
    {
        if (type == typeof(string))
        {
            return string.Empty;
        }

        var collection = type
            .GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType &&
                                 x.GetGenericTypeDefinition() == typeof(ICollection<>) &&
                                 x.GetGenericArguments().Length == 1);
        if (collection != null)
        {
            var collectionType = collection.GetGenericArguments().First();
            var item = GetDefaultValue(collectionType);
            var listType = typeof(List<>).MakeGenericType(collectionType);
            var list = Activator.CreateInstance(listType);
            listType.GetMethod("Add")?.Invoke(list, [item]);
            return list;
        }

        if (type.GetConstructor(Type.EmptyTypes) != null)
        {
            return InitializeClass(type);
        }

        return type.IsValueType
            ? Activator.CreateInstance(type)
            : null;
    }
}