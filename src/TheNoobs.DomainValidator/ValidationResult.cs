using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions;
using TheNoobs.DomainValidator.Abstractions.Rules;
using TheNoobs.DomainValidator.Rules;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator;

public class ValidationResult<TEntity> : IValidationResult<TEntity>
{
    private readonly ConcurrentDictionary<ValidationResultCode, IRule<TEntity>> _rules;
    private IReadOnlyCollection<IProblem>? _problems;

    internal ValidationResult(TEntity entity)
    {
        _rules = new ConcurrentDictionary<ValidationResultCode, IRule<TEntity>>();
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public TEntity Entity { get; }

    public bool IsValid(out IReadOnlyCollection<IProblem> problems)
    {
        _problems = _rules.Values.Where(rule => !rule.IsSatisfiedBy(Entity)).ToList();
        problems = _problems;
        return !_problems.Any();
    }

    /// <inheritdoc />
    public IValidationResult<TEntity> AddRule(ValidationResultCode code, ValidationResultDescription description, Expression<Func<TEntity, bool>> expression)
    {
        var added = _rules.TryAdd(code, new ExpressionRule<TEntity>(code, description, expression));
        if (!added)
        {
            throw new ArgumentException($"The code {code} already exists.", nameof(code));
        }

        return this;
    }

    /// <inheritdoc />
    public IValidationResult<TEntity> AddRule(ValidationResultCode code, ValidationResultDescription description, IRuleSpecification<TEntity> specification)
    {
        var added = _rules.TryAdd(code, new SpecificationRule<TEntity>(code, description, specification));
        if (!added)
        {
            throw new ArgumentException($"The code {code} already exists.", nameof(code));
        }

        return this;
    }
}
