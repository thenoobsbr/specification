namespace TheNoobs.Specification.UnitTests.Stubs.Entitities;

public class Shareholder
{
    public Shareholder(string firstName, DateTime birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("Shareholder first name cannot be null or whitespace.", nameof(firstName));
        }

        if (birthDate > DateTime.UtcNow.AddYears(-18))
        {
            throw new ArgumentException("Shareholder should be at least 18 years old.", nameof(birthDate));
        }

        FirstName = firstName;
        BirthDate = birthDate;
    }

    public DateTime BirthDate { get; }
    public ShareholderDocument? Document { get; set; }
    public string FirstName { get; }
    public string? LastName { get; set; }
}
