using SchemeGenerator.Test.Models;
using SchemeGenerator.Xunit;

namespace SchemeGenerator.Test;

public class XunitIntegrationTests
{
    [Theory]
    [AutoSchemeData<SimpleType>]
    public void AutoSchemeData_ProvidesInstance(SimpleType model)
    {
        Assert.NotNull(model);
        Assert.Equal(0, model.Number);
    }

    [Theory]
    [SchemeGeneratorData(typeof(ComplexType))]
    public void SchemeGeneratorData_ProvidesInstance(ComplexType model)
    {
        Assert.NotNull(model);
        Assert.NotEmpty(model.Objects);
    }
}
