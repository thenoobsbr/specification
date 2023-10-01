using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.UnitTests.Stubs.Entitities;

namespace TheNoobs.Specification.UnitTests.Stubs.Rules;

public class IsValidShareholderValidityDocumentRule : IRule<ShareholderDocument>
{
    public bool IsSatisfiedBy(ShareholderDocument entity)
    {
        return entity.Type switch
        {
            DocumentType.Id => entity.Validity >= DateTime.UtcNow,
            DocumentType.Passport => entity.Validity >= DateTime.UtcNow.AddMonths(6),
            _ => true
        };
    }
}

