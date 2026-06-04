using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchemeGenerator.Test.Generated;
using SchemeGenerator.Test.Models;

namespace SchemeGenerator.Test;

public class SchemeGeneratorTests
{
    private readonly global::SchemeGenerator.SchemeGenerator _generator = new();

    [Fact]
    public void GetDefaultJson_ComplexType_Deserializes()
    {
        var json = _generator.GetDefaultJson(typeof(ComplexType));
        var obj = JsonSerializer.Deserialize<ComplexType>(json);
        Assert.NotNull(obj);
        Assert.Single(obj!.Numbers);
        Assert.Single(obj.Objects);
    }

    [Fact]
    public void GetDefaultJson_String_ReturnsNonEmpty()
    {
        var json = _generator.GetDefaultJson(typeof(string));
        Assert.NotEmpty(json);
    }

    [Fact]
    public void GetDefaultJson_ListInt_ContainsZero()
    {
        var json = _generator.GetDefaultJson(typeof(List<int>));
        var list = JsonSerializer.Deserialize<List<int>>(json);
        Assert.Contains(list!, x => x == 0);
    }

    [Fact]
    public void GetDefault_Generic_ReturnsInstance()
    {
        var value = _generator.GetDefault<SimpleType>();
        Assert.NotNull(value);
        Assert.Equal(0, value!.Number);
    }

    [Fact]
    public void GetDefaultJson_Generic_MatchesTypeBased()
    {
        Assert.Equal(_generator.GetDefaultJson(typeof(SimpleType)), _generator.GetDefaultJson<SimpleType>());
    }

    [Fact]
    public void GetDefault_Enum_ReturnsFirstValue()
    {
        var value = (Status)_generator.GetDefaultValue(typeof(Status))!;
        Assert.Equal(Status.Active, value);
    }

    [Fact]
    public void GetDefault_Dictionary_HasEntry()
    {
        var value = (WithDictionary)_generator.GetDefaultValue(typeof(WithDictionary))!;
        Assert.Single(value.Values);
    }

    [Fact]
    public void GetDefault_Array_HasConfiguredCount()
    {
        var value = (WithArray)_generator.GetDefaultValue(typeof(WithArray))!;
        Assert.Single(value.Numbers);
    }

    [Fact]
    public void GetDefault_Nullable_HasValue()
    {
        var value = (WithNullable)_generator.GetDefaultValue(typeof(WithNullable))!;
        Assert.NotNull(value.Count);
    }

    [Fact]
    public void Populate_FillsExistingInstance()
    {
        var target = new SimpleType();
        _generator.Populate(target);
        Assert.Equal(0, target.Number);
        Assert.Equal(string.Empty, target.Text);
    }

    [Fact]
    public void Attributes_SchemeIgnoreAndSchemeValue_Applied()
    {
        var value = (AttributedType)_generator.GetDefaultValue(typeof(AttributedType))!;
        Assert.Equal("fixed", value.FixedText);
        Assert.Equal(string.Empty, value.Ignored);
        Assert.Equal(3, value.Counted.Count);
    }

    [Fact]
    public void SampleMode_StringIsNotEmpty()
    {
        var gen = new global::SchemeGenerator.SchemeGenerator(new SchemeGeneratorOptions
        {
            PrimitiveMode = PrimitiveDefaultsMode.Sample
        });
        Assert.Equal("sample", gen.GetDefault<string>());
    }

    [Fact]
    public void CyclicGraph_DoesNotStackOverflow()
    {
        var value = _generator.GetDefault<CyclicNode>();
        Assert.NotNull(value);
    }

    [Fact]
    public void RecordWithCtor_CreatesInstance()
    {
        var value = _generator.GetDefault<RecordWithCtor>();
        Assert.NotNull(value);
        Assert.Equal(string.Empty, value!.Name);
    }

    [Fact]
    public void CollectionItemCount_OptionsRespected()
    {
        var gen = new global::SchemeGenerator.SchemeGenerator(new SchemeGeneratorOptions { CollectionItemCount = 2 });
        var list = (List<int>)gen.GetDefaultValue(typeof(List<int>))!;
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public void AddSchemeGenerator_WithConfigure_UsesOptions()
    {
        var services = new ServiceCollection();
        services.AddSchemeGenerator(o => o.CollectionItemCount = 4);
        using var provider = services.BuildServiceProvider();
        var gen = provider.GetRequiredService<ISchemeGenerator>();
        var list = (List<int>)gen.GetDefaultValue(typeof(List<int>))!;
        Assert.Equal(4, list.Count);
    }

    [Fact]
    public void AddSchemeGenerator_RegistersResolvableService()
    {
        var services = new ServiceCollection();
        services.AddSchemeGenerator();
        using var provider = services.BuildServiceProvider();
        var generator = provider.GetRequiredService<ISchemeGenerator>();
        Assert.IsType<global::SchemeGenerator.SchemeGenerator>(generator);
    }

    [Fact]
    public void SourceGenerator_Create_ReturnsPopulatedDto()
    {
        var order = GeneratedOrder.GeneratedOrderSchemeDefaults.Create();
        Assert.Equal(0, order.Id);
        Assert.Equal(string.Empty, order.Sku);
    }

    [Fact]
    public void SourceGenerator_CreateJson_IsValidJson()
    {
        var json = GeneratedOrder.GeneratedOrderSchemeDefaults.CreateJson();
        var order = JsonSerializer.Deserialize<GeneratedOrder>(json);
        Assert.NotNull(order);
    }
}
