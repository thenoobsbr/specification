using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.Internals;
using TheNoobs.Specification.UnitTests.Stubs.Entitities;

namespace TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders;

public class CircuitBreakerIsShareholderAbleBeSentSpecification : ISpecification<Shareholder>
{
    private readonly ISpecification<Shareholder> _specification;
    
    public CircuitBreakerIsShareholderAbleBeSentSpecification()
    {
        _specification = SpecificationFactory<Shareholder>
            .CircuitBreaker()
            .Requires("SH001", "Shareholder last name is required.", s => !string.IsNullOrEmpty(s.LastName))
            .And(s => s.Document != null)
            .WithCodeAndDescription("SH002", "Shareholder document cannot be null.")
            .Build();
    }
    
    public bool IsSatisfiedBy(Shareholder entity, out IEnumerable<IIssue> issues)
    {
        return _specification.IsSatisfiedBy(entity, out issues);
    }
}

