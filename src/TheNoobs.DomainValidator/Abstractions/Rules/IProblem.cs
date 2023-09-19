using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions.Rules;

public interface IProblem
{
    /// <summary>
    ///     Gets the problem code.
    /// </summary>
    ValidationResultCode Code { get; }

    /// <summary>
    ///     Gets the problem description.
    /// </summary>
    ValidationResultDescription Description { get; }
}
