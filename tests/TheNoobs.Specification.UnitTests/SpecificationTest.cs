using FluentAssertions;
using TheNoobs.Specification.UnitTests.Stubs.Entitities;
using TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders;
using Xunit;

namespace TheNoobs.Specification.UnitTests;

[Trait("Category", "UnitTest")]
[Trait("Class", nameof(SpecificationBuilder<Shareholder>))]
public class SpecificationTest
{
    [Fact]
    public void Test1()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new IsShareholderAbleBeSentSpecification();

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCountGreaterThan(1);
    }
    
    [Fact]
    public void Test2()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new IsShareholderAbleBeSentSpecification(SpecificationBehavior.CircuitBreaker);

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCount(1);
    }
}

