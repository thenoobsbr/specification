using System.Diagnostics.CodeAnalysis;
using TheNoobs.DomainValidator.Rules;

namespace TheNoobs.DomainValidator.UnitTests.Stubs;

public class Shareholder
{
    public DateTime BirthDate { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ShareholderDocument? Document { get; set; } = null!;

    public DomainValidator DoValidation()
    {
        var domainValidator = new DomainValidator();
        domainValidator
            .For(this)
            .AddRule("SHR001", "Shareholders should be of legal age.", x => x.BirthDate <= DateTime.UtcNow.AddYears(-18))
            .AddRule("SHR002", "Shareholders should be the first name.", new IsNotEmptyRuleSpecification<Shareholder>(shareholder => shareholder.FirstName))
            .AddRule("SHR003", "Shareholders should be the last name.", x => !string.IsNullOrWhiteSpace(x.LastName))
            .AddRule("SHR004", "Shareholder should be a document.", new IsNotNullRuleSpecification<Shareholder>(x => x.Document));
        
        return Document?.DoValidation(domainValidator) ?? domainValidator;
    }
}
