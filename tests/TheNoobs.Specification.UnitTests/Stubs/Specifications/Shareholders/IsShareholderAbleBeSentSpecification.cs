using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.Internals;
using TheNoobs.Specification.UnitTests.Stubs.Entitities;

namespace TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders;

public class IsShareholderAbleBeSentSpecification : ISpecification<Shareholder>
{
    private readonly ISpecification<Shareholder> _specification;
    
    public IsShareholderAbleBeSentSpecification(SpecificationBehavior behavior = SpecificationBehavior.NonCircuitBreaker)
    {
        _specification = new SpecificationBuilder<Shareholder>(behavior)
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

