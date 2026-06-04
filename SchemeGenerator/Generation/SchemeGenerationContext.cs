namespace SchemeGenerator.Generation;

internal sealed class SchemeGenerationContext(SchemeGeneratorOptions options)
{
    public SchemeGeneratorOptions Options { get; } = options;
    public int Depth { get; private set; }

    private readonly HashSet<Type> _visitedTypes = [];

    public bool TryEnter(Type type)
    {
        if (Depth >= Options.MaxDepth)
        {
            return false;
        }

        if (!_visitedTypes.Add(type))
        {
            return false;
        }

        Depth++;
        return true;
    }

    public void Exit(Type type)
    {
        Depth--;
        _visitedTypes.Remove(type);
    }
}
