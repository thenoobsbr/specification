using FluentAssertions;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.UnitTests.Stubs.Entitities;
using TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders;
using Xunit;

namespace TheNoobs.Specification.UnitTests;

[Trait("Category", "UnitTest")]
[Trait("Class", nameof(SpecificationBuilder<Shareholder>))]
public class SpecificationTest
{
    [Fact]
    public void Given_AnInvalidObjectAndACircuitBreakerSpecification_When_ITryToCheckIfIsSatisfied_Then_TheSpecificationShouldNotBeSatisfiedAndOnlyTheFirstIssuesShouldBeReported()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new CircuitBreakerIsShareholderAbleBeSentSpecification();

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCount(1);
    }

    [Fact]
    public void Given_AnInvalidObjectAndANonCircuitBreakerSpecification_When_ITryToCheckIfIsSatisfied_Then_TheSpecificationShouldNotBeSatisfiedAndAllIssuesShouldBeReported()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new NonCircuitBreakerIsShareholderAbleBeSentSpecification();

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCountGreaterThan(1);
    }
}
