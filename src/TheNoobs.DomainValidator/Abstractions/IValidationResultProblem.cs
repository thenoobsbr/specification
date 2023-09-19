using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions;

public interface IValidationResultProblem
{
    /// <summary>
    ///     Gets the validation result code.
    /// </summary>
    ValidationResultCode Code { get; }

    /// <summary>
    ///     Gets the validation result description.
    /// </summary>
    ValidationResultDescription Description { get; }
}
