using TheNoobs.DomainValidator.Abstractions.Rules;

namespace TheNoobs.DomainValidator.UnitTests.Stubs;

public class ShareholderDocument
{
    public string Number { get; set; } = null!;

    public DocumentType Type { get; set; }

    public DateTime Validity { get; set; }

    public DomainValidator DoValidation(DomainValidator domainValidator)
    {
        domainValidator
            .For(this)
            .AddRule("SHRDOC001", "Shareholder document should be valid.", new ShareholderDocumentValiditySpecification());

        return domainValidator;
    }
    
    private class ShareholderDocumentValiditySpecification : IRuleSpecification<ShareholderDocument>
    {
        public bool IsSatisfiedBy(ShareholderDocument entity)
        {
            if (entity.Type == DocumentType.Id)
            {
                return entity.Validity >= DateTime.UtcNow;
            }

            if (entity.Type == DocumentType.Passport)
            {
                return entity.Validity >= DateTime.UtcNow.AddMonths(6);
            }

            return true;

        }
    }
}
