using System.Text.Json;
using SchemeGenerator.Test.Models;

namespace SchemeGenerator.Test;

public class Tests
{
    [Fact]
    public void WhenUsingJson()
    {
        var generator = new SchemeGenerator();
        var json = generator.GetDefaultJson(typeof(ComplexType));
        var obj = JsonSerializer.Deserialize<ComplexType>(json);
        Assert.NotNull(obj);
    }
    
    [Fact]
    public void WhenUsingString()
    {
        var generator = new SchemeGenerator();
        var json = generator.GetDefaultJson(typeof(string));
        Assert.NotEmpty(json);
    }
    
    [Fact]
    public void WhenUsingList()
    {
        var generator = new SchemeGenerator();
        var json = generator.GetDefaultJson(typeof(List<int>));
        var list = JsonSerializer.Deserialize<List<int>>(json);
        Assert.Contains(list!, x => x == 0);
    }
}