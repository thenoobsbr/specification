using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator;

public class ValidationResult<TEntity> : IValidationResult<TEntity>
{
    private readonly ConcurrentDictionary<ValidationResultCode, IRule<TEntity>> _rules;
    private IEnumerable<ValidationResultProblem>? _problems;

    internal ValidationResult(TEntity entity)
    {
        _rules = new ConcurrentDictionary<ValidationResultCode, IRule<TEntity>>();
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public TEntity Entity { get; }

    public IEnumerable<IValidationResultProblem> GetProblems()
    {
        if (_problems is null)
        {
            return new List<IValidationResultProblem>(0);
        }

        return _problems;
    }

    /// <summary>
    ///     Executes the validation and returns the result.
    /// </summary>
    public bool IsSatisfied()
    {
        _problems = _rules.Values.Where(rule => !rule.IsSatisfiedBy(Entity)).Select(rule => new ValidationResultProblem(rule));
        return !_problems.Any();
    }

    public IValidationResult<TEntity> AddRule(ValidationResultCode code, ValidationResultDescription description, Expression<Func<TEntity, bool>> expression)
    {
        var added = _rules.TryAdd(code, new ExpressionRule<TEntity>(code, description, expression));
        if (!added)
        {
            throw new ArgumentException($"The code {code} already exists.", nameof(code));
        }

        return this;
    }

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
