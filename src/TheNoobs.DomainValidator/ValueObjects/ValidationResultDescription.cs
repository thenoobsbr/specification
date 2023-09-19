using System;

namespace TheNoobs.DomainValidator.ValueObjects;

public record ValidationResultDescription
{
    public ValidationResultDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
        }

        Value = description;
    }

    public string Value { get; }

    public static implicit operator string(ValidationResultDescription description)
    {
        return description.Value;
    }

    public static implicit operator ValidationResultDescription(string description)
    {
        return new ValidationResultDescription(description);
    }
}
