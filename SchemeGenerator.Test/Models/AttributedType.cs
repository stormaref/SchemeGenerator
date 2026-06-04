using SchemeGenerator.Attributes;

namespace SchemeGenerator.Test.Models;

public class AttributedType
{
    [SchemeIgnore]
    public string Ignored { get; set; } = string.Empty;

    [SchemeValue("fixed")]
    public string FixedText { get; set; } = string.Empty;

    [SchemeCount(3)]
    public List<int> Counted { get; set; } = [];
}
