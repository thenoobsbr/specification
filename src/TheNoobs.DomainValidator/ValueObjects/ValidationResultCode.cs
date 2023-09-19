using System;

namespace TheNoobs.DomainValidator.ValueObjects;

public record ValidationResultCode
{
    public ValidationResultCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(code));
        }

        Value = code;
    }

    public string Value { get; }

    public static implicit operator string(ValidationResultCode code)
    {
        return code.Value;
    }

    public static implicit operator ValidationResultCode(string code)
    {
        return new ValidationResultCode(code);
    }
}
