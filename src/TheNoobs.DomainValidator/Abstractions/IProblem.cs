using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions;

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
