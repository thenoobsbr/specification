using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification.Abstractions;

public interface IIssue
{
    SpecificationCode Code { get; }

    SpecificationDescription Description { get; }
}
