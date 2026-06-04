using System.Reflection;
using System.Runtime.CompilerServices;

namespace SchemeGenerator.Generation;

internal static class ObjectTypeHandler
{
    public static object? TryCreate(
        Type type,
        SchemeGenerationContext context,
        Func<Type, object?> generate)
    {
        if (!context.TryEnter(type))
        {
            return null;
        }

        try
        {
            object? instance = null;

            var parameterless = type.GetConstructor(Type.EmptyTypes);
            if (parameterless is not null)
            {
                instance = Activator.CreateInstance(type);
            }
            else
            {
                instance = TryCreateFromConstructor(type, generate);
            }

            if (instance is null)
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }

            PopulateProperties(instance, type, context.Options, generate);
            return instance;
        }
        finally
        {
            context.Exit(type);
        }
    }

    private static object? TryCreateFromConstructor(Type type, Func<Type, object?> generate)
    {
        var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .OrderBy(c => c.GetParameters().Length)
            .FirstOrDefault();

        if (ctor is null)
        {
            return null;
        }

        var args = ctor.GetParameters()
            .Select(p => generate(p.ParameterType))
            .ToArray();

        return ctor.Invoke(args);
    }

    public static void PopulateProperties(
        object instance,
        Type type,
        SchemeGeneratorOptions options,
        Func<Type, object?> generate)
    {
        foreach (var info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var meta = PropertyGenerationInfo.From(info);
            if (meta.Ignored)
            {
                continue;
            }

            if (meta.FixedValue is not null)
            {
                TrySetProperty(instance, info, meta.FixedValue);
                continue;
            }

            if (!CanWrite(info))
            {
                continue;
            }

            var count = meta.CountOverride ?? options.CollectionItemCount;
            var value = generateWithCount(info.PropertyType, count, generate);
            TrySetProperty(instance, info, value);
        }
    }

    private static object? generateWithCount(Type propertyType, int count, Func<Type, object?> generate)
    {
        if (ArrayTypeHandler.IsArray(propertyType))
        {
            return ArrayTypeHandler.Create(propertyType, count, generate);
        }

        if (DictionaryTypeHandler.TryGetDictionaryTypes(propertyType, out var keyType, out var valueType))
        {
            return DictionaryTypeHandler.Create(propertyType, keyType, valueType, generate);
        }

        if (CollectionTypeHandler.TryGetCollectionElementType(propertyType, out var elementType))
        {
            return CollectionTypeHandler.Create(propertyType, elementType, count, generate);
        }

        return generate(propertyType);
    }

    private static bool CanWrite(PropertyInfo property)
    {
        if (property.CanWrite)
        {
            return true;
        }

        return property.GetSetMethod(nonPublic: true) is not null;
    }

    private static void TrySetProperty(object instance, PropertyInfo property, object? value)
    {
        var setter = property.GetSetMethod(nonPublic: true) ?? property.GetSetMethod();
        if (setter is null)
        {
            return;
        }

        setter.Invoke(instance, [value]);
    }
}
