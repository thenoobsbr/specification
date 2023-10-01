using System.Collections.Generic;

namespace TheNoobs.Specification.Abstractions;

public interface ISpecification<in TEntity>
{
    /// <summary>
    ///     Checks if the specification is satisfied by the entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="issues">Empty when is satisfied.</param>
    /// <returns></returns>
    bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues);
}
