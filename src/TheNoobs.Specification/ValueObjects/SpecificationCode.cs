using System;

namespace TheNoobs.Specification.ValueObjects;

public record SpecificationCode
{
    public SpecificationCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(code));
        }

        Value = code;
    }

    public string Value { get; }

    public static implicit operator string(SpecificationCode code)
    {
        return code.Value;
    }

    public static implicit operator SpecificationCode(string code)
    {
        return new SpecificationCode(code);
    }

    public override string ToString()
    {
        return Value;
    }
}
