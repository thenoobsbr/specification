using System.Diagnostics.CodeAnalysis;

namespace TheNoobs.DomainValidator.UnitTests.Stubs;

public class Shareholder
{
    public DateTime BirthDate { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ShareholderDocument Document { get; set; } = null!;

    public void DoValidation(out DomainValidator domainValidator)
    {
        domainValidator = new();
        domainValidator
            .For(this)
            .AddRule("SHR001", "Shareholders should be of legal age.", x => x.BirthDate <= DateTime.UtcNow.AddYears(-18))
            .AddRule("SHR002", "Shareholders should be the first name.", x => !string.IsNullOrWhiteSpace(x.FirstName))
            .AddRule("SHR003", "Shareholders should be the last name.", x => !string.IsNullOrWhiteSpace(x.LastName))
            .AddRule("SHR004", "Shareholder should be a document.", x => x.Document != null);
        
        Document.DoValidation(domainValidator);
    }
}
