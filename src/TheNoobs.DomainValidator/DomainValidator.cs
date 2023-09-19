using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TheNoobs.DomainValidator.Abstractions;
using TheNoobs.DomainValidator.Abstractions.Rules;

namespace TheNoobs.DomainValidator;

public class DomainValidator
{
    private readonly ConcurrentDictionary<Type, IValidationResult> _validationResults;
    private IReadOnlyCollection<IProblem>? _problems;

    public DomainValidator()
    {
        _validationResults = new ConcurrentDictionary<Type, IValidationResult>();
    }

    public IValidationResult<TEntity> For<TEntity>(TEntity entity)
    {
        var validationResult = new ValidationResult<TEntity>(entity);
        var added = _validationResults.TryAdd(typeof(TEntity), validationResult);
        if (!added)
        {
            throw new ArgumentException($"The entity {typeof(TEntity)} already exists.", nameof(entity));
        }

        return validationResult;
    }

    public IReadOnlyCollection<IProblem> GetProblems()
    {
        return _problems ?? Array.Empty<IProblem>();
    }

    public bool IsValid()
    {
        foreach (var validationResult in _validationResults.Values)
        {
            validationResult.IsValid();
        }

        _problems = _validationResults.SelectMany(v => v.Value.GetProblems()).ToList();
        return !_problems.Any();
    }

    public bool IsValid(out IReadOnlyCollection<IProblem> problems)
    {
        foreach (var validationResult in _validationResults.Values)
        {
            validationResult.IsValid();
        }

        _problems = _validationResults.SelectMany(v => v.Value.GetProblems()).ToList();
        problems = _problems;
        return _problems.Any();
    }
}
