using BenchmarkDotNet.Attributes;

namespace SchemeGenerator.Benchmarks;

[MemoryDiagnoser]
public class GenerationBenchmarks
{
    private readonly global::SchemeGenerator.SchemeGenerator _reflection = new();

    [Benchmark]
    public BenchmarkDto Reflection() => _reflection.GetDefault<BenchmarkDto>()!;

    [Benchmark]
    public BenchmarkDto SourceGenerated() => BenchmarkDto.BenchmarkDtoSchemeDefaults.Create();
}
