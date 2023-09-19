using System;
using TheNoobs.DomainValidator.Abstractions;
using TheNoobs.DomainValidator.Abstractions.Rules;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator;

public class ValidationResultProblem : IValidationResultProblem
{
    internal ValidationResultProblem(IRule rule)
    {
        Code = rule.Code;
        Description = rule.Description;
    }

    public ValidationResultCode Code { get; }
    public ValidationResultDescription Description { get; }
}
