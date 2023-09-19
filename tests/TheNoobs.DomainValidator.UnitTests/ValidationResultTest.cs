using FluentAssertions;
using TheNoobs.DomainValidator.Abstractions;
using TheNoobs.DomainValidator.UnitTests.Stubs;
using Xunit;

namespace TheNoobs.DomainValidator.UnitTests;

[Trait("Category", "UnitTests")]
[Trait("Class", nameof(ValidationResult<Shareholder>))]
public class ValidationResultTest
{
    [Fact]
    public void Test1()
    {
        var shareholder = new Shareholder();
        IValidationResult<Shareholder> validationResult = new ValidationResult<Shareholder>(shareholder);
        validationResult.IsValid(out var problems).Should().BeTrue();
        problems.Should().BeEmpty();

        shareholder.BirthDate = new DateTime(2021, 01, 01);
        validationResult.AddRule("SHR001", "Shareholders should be of legal age.", x => x.BirthDate <= DateTime.UtcNow.AddYears(-18));
        validationResult.IsValid(out problems).Should().BeFalse();
        problems.Should().HaveCount(1);
        problems.First().Code.Value.Should().Be("SHR001");
        problems.First().Description.Value.Should().Be("Shareholders should be of legal age.");

        shareholder.BirthDate = new DateTime(1984, 03, 26);
        validationResult.IsValid(out problems).Should().BeTrue();
        problems.Should().BeEmpty();

        validationResult
            .AddRule("SHR002", "Shareholders should be the first name.", x => !string.IsNullOrWhiteSpace(x.FirstName))
            .AddRule("SHR003", "Shareholders should be the last name.", x => !string.IsNullOrWhiteSpace(x.LastName));

        validationResult.IsValid(out problems).Should().BeFalse();
        problems.Should().HaveCount(2);
        problems.First(x => x.Code == "SHR002").Code.Value.Should().Be("SHR002");
        problems.First(x => x.Code == "SHR002").Description.Value.Should().Be("Shareholders should be the first name.");
        problems.Last(x => x.Code == "SHR003").Code.Value.Should().Be("SHR003");
        problems.Last(x => x.Code == "SHR003").Description.Value.Should().Be("Shareholders should be the last name.");

        shareholder.FirstName = "John";
        validationResult.IsValid(out problems).Should().BeFalse();
        problems.Should().HaveCount(1);
        problems.First().Code.Value.Should().Be("SHR003");
        problems.First().Description.Value.Should().Be("Shareholders should be the last name.");

        shareholder.LastName = "Wick";
        validationResult.IsValid(out problems).Should().BeTrue();
        problems.Should().BeEmpty();
    }

    [Fact]
    public void Test2()
    {
        var shareholder = new Shareholder
        {
            BirthDate = new DateTime(1984, 03, 26),
            FirstName = string.Empty,
            LastName = "Wick"
        };

        var validator = shareholder.DoValidation();
        validator.IsValid(out var problems).Should().BeFalse();
        problems.Should().HaveCount(2);
        problems.Any(x => x.Code == "SHR002").Should().BeTrue();
        problems.Any(x => x.Code == "SHR004").Should().BeTrue();
        shareholder.FirstName = "John";
        validator = shareholder.DoValidation();
        validator.IsValid(out problems).Should().BeFalse();
        problems.Should().HaveCount(1);
        problems.First().Code.Value.Should().Be("SHR004");
        shareholder.Document = new ShareholderDocument();
        shareholder.Document.Type = DocumentType.Id;
        shareholder.Document.Number = "123456789";
        validator = shareholder.DoValidation();
        validator.IsValid(out problems).Should().BeFalse();
        problems.Should().HaveCount(1);
        problems.First().Code.Value.Should().Be("SHRDOC002");
    }
}
