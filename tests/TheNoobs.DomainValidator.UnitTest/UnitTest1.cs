using Xunit;

namespace TheNoobs.DomainValidator.UnitTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Class1 class1 = new();
        Assert.Equal("Something", class1.DoSomething());
    }
}
