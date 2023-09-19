using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions;

public interface IValidationResultProblem
{
    ValidationResultCode Code { get; }
    ValidationResultDescription Description { get; }
}
