using TheNoobs.DomainValidator.Abstractions.Rules;

namespace TheNoobs.DomainValidator.UnitTests.Stubs;

public class ShareholderDocument
{
    public string Number { get; set; } = null!;

    public DocumentType Type { get; set; }

    public DateTime Validity { get; set; }

    public void DoValidation(DomainValidator domainValidator)
    {
        domainValidator
            .For(this)
            .AddRule("SHRDOC001", "Shareholder document should be valid.", new ShareholderDocumentValiditySpecification());
    }
    
    private class ShareholderDocumentValiditySpecification : IRuleSpecification<ShareholderDocument>
    {
        public bool IsSatisfiedBy(ShareholderDocument entity)
        {
            return entity.Validity >= DateTime.UtcNow;
        }
    }
}
