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
    public void Test()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new IsShareholderAbleBeSentSpecification();

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCountGreaterThan(1);
    }
}

