using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions.Rules;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions;

public interface IValidationResult
{
    /// <summary>
    ///     Gets the list of validation problems.
    /// </summary>
    IEnumerable<IValidationResultProblem> GetProblems();

    /// <summary>
    ///     Executes the validation and returns the result.
    /// </summary>
    bool IsSatisfied();
}

public interface IValidationResult<TEntity> : IValidationResult
{
    /// <summary>
    ///     Gets the entity.
    /// </summary>
    TEntity Entity { get; }

    /// <summary>
    ///     Adds a rule to the validation result.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="description"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    IValidationResult<TEntity> AddRule(ValidationResultCode code, ValidationResultDescription description, Expression<Func<TEntity, bool>> expression);

    /// <summary>
    ///     Adds a rule to the validation result.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="description"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    IValidationResult<TEntity> AddRule(ValidationResultCode code, ValidationResultDescription description, IRuleSpecification<TEntity> specification);
}
