using System;

namespace TheNoobs.Specification.Exceptions;

public class SpecificationBehaviorNotSupportedException : Exception
{
    public SpecificationBehaviorNotSupportedException(SpecificationBehavior behavior)
        : base($"Specification behavior {behavior} is not supported.")
    {
    }
}
