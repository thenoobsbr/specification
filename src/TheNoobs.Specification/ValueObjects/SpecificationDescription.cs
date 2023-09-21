using System;

namespace TheNoobs.Specification.ValueObjects;

public record SpecificationDescription
{
    public SpecificationDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
        }

        Value = description;
    }

    public string Value { get; }

    public static implicit operator string(SpecificationDescription description)
    {
        return description.Value;
    }

    public static implicit operator SpecificationDescription(string description)
    {
        return new SpecificationDescription(description);
    }

    public override string ToString()
    {
        return Value;
    }
}
