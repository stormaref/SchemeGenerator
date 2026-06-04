using SchemeGenerator.Attributes;

namespace SchemeGenerator.Benchmarks;

[GenerateSchemeDefaults]
public partial class BenchmarkDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
