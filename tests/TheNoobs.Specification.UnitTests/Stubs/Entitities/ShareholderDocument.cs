namespace TheNoobs.Specification.UnitTests.Stubs.Entitities;

public class ShareholderDocument
{
    public string Number { get; set; } = null!;

    public DocumentType Type { get; set; }

    public DateTime Validity { get; set; }
}
