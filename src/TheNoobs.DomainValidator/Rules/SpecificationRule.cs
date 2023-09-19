using System;
using TheNoobs.DomainValidator.Abstractions.Rules;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Rules;

public class SpecificationRule<TEntity> : IRule<TEntity>
{
    private readonly IRuleSpecification<TEntity> _specification;

    internal SpecificationRule(ValidationResultCode code, ValidationResultDescription description, IRuleSpecification<TEntity> specification)
    {
        _specification = specification ?? throw new ArgumentNullException(nameof(specification));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    /// <inheritdoc />
    public ValidationResultCode Code { get; }

    /// <inheritdoc />
    public ValidationResultDescription Description { get; }

    /// <inheritdoc />
    public bool IsSatisfiedBy(TEntity entity)
    {
        return _specification.IsSatisfiedBy(entity);
    }
}
