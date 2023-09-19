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
        validationResult.GetProblems().Should().BeEmpty();
        validationResult.IsValid().Should().BeTrue();
        validationResult.GetProblems().Should().BeEmpty();

        shareholder.BirthDate = new DateTime(2021, 01, 01);
        validationResult.AddRule("SHR001", "Shareholders should be of legal age.", x => x.BirthDate <= DateTime.UtcNow.AddYears(-18));
        validationResult.IsValid().Should().BeFalse();
        validationResult.GetProblems().Should().HaveCount(1);
        validationResult.GetProblems().First().Code.Value.Should().Be("SHR001");
        validationResult.GetProblems().First().Description.Value.Should().Be("Shareholders should be of legal age.");

        shareholder.BirthDate = new DateTime(1984, 03, 26);
        validationResult.IsValid().Should().BeTrue();
        validationResult.GetProblems().Should().BeEmpty();

        validationResult
            .AddRule("SHR002", "Shareholders should be the first name.", x => !string.IsNullOrWhiteSpace(x.FirstName))
            .AddRule("SHR003", "Shareholders should be the last name.", x => !string.IsNullOrWhiteSpace(x.LastName));

        validationResult.IsValid().Should().BeFalse();
        validationResult.GetProblems().Should().HaveCount(2);
        validationResult.GetProblems()
            .First(x => x.Code == "SHR002")
            .Code.Value.Should().Be("SHR002");
        validationResult.GetProblems()
            .First(x => x.Code == "SHR002")
            .Description.Value.Should().Be("Shareholders should be the first name.");
        validationResult.GetProblems()
            .Last(x => x.Code == "SHR003")
            .Code.Value.Should().Be("SHR003");
        validationResult.GetProblems()
            .Last(x => x.Code == "SHR003")
            .Description.Value.Should().Be("Shareholders should be the last name.");

        shareholder.FirstName = "John";
        validationResult.IsValid(out var problems).Should().BeFalse();
        problems.Should().HaveCount(1);
        problems.First().Code.Value.Should().Be("SHR003");
        validationResult.GetProblems().First()
            .Description.Value.Should().Be("Shareholders should be the last name.");

        shareholder.LastName = "Wick";
        validationResult.IsValid().Should().BeTrue();
        validationResult.GetProblems().Should().BeEmpty();
    }
}
