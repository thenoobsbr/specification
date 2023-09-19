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

    public bool IsValid(out IReadOnlyCollection<IProblem> problems)
    {
        var problemList  = new List<IProblem>();
        foreach (var validationResult in _validationResults.Values)
        {
            validationResult.IsValid(out var validationProblems);
            problemList.AddRange(validationProblems);
        }

        _problems = problems = problemList;
        return !_problems.Any();
    }
}
