using SchemeGenerator.Attributes;

namespace SchemeGenerator.Test.Generated;

[GenerateSchemeDefaults]
public partial class GeneratedOrder
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
}
