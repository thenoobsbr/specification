using System.Diagnostics.CodeAnalysis;

namespace TheNoobs.DomainValidator.UnitTests.Stubs;

[ExcludeFromCodeCoverage]
public class Shareholder
{
    public DateTime BirthDate { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
