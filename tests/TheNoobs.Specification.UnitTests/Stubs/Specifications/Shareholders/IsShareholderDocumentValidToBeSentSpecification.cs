using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.UnitTests.Stubs.Entitities;
using TheNoobs.Specification.UnitTests.Stubs.Rules;

namespace TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders;

public class IsShareholderDocumentValidToBeSentSpecification : ISpecification<ShareholderDocument>
{
    private readonly ISpecification<ShareholderDocument> _specification;

    public IsShareholderDocumentValidToBeSentSpecification()
    {
        _specification = new SpecificationBuilder<ShareholderDocument>(SpecificationBehavior.NonCircuitBreaker)
            .Requires("SHD001", "Shareholder document outside of validity.", new IsValidShareholderValidityDocumentRule())
            .Build();
    }

    public bool IsSatisfiedBy(ShareholderDocument entity, out IEnumerable<IIssue> issues)
    {
        return _specification.IsSatisfiedBy(entity, out issues);
    }
}
