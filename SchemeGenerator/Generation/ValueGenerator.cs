namespace SchemeGenerator.Generation;

internal sealed class ValueGenerator(SchemeGeneratorOptions options)
{
    private readonly SchemeGenerationContext _context = new(options);

    public object? Generate(Type type) => GenerateCore(type);

    public void Populate(object instance)
    {
        ObjectTypeHandler.PopulateProperties(instance, instance.GetType(), options, GenerateCore);
    }

    private object? GenerateCore(Type type)
    {
        if (NullableTypeHandler.IsNullableType(type, out var underlying))
        {
            return underlying is null ? null : GenerateCore(underlying);
        }

        if (PrimitiveValueFactory.IsPrimitiveType(type))
        {
            return PrimitiveValueFactory.Create(type, options);
        }

        if (EnumTypeHandler.IsEnumType(type))
        {
            return EnumTypeHandler.Create(type, options);
        }

        if (ArrayTypeHandler.IsArray(type))
        {
            return ArrayTypeHandler.Create(type, options.CollectionItemCount, GenerateCore);
        }

        if (DictionaryTypeHandler.TryGetDictionaryTypes(type, out var keyType, out var valueType))
        {
            return DictionaryTypeHandler.Create(type, keyType, valueType, GenerateCore);
        }

        if (CollectionTypeHandler.TryGetCollectionElementType(type, out var elementType))
        {
            return CollectionTypeHandler.Create(type, elementType, options.CollectionItemCount, GenerateCore);
        }

        return ObjectTypeHandler.TryCreate(type, _context, GenerateCore);
    }
}
