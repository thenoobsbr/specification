using System.Collections.Generic;

namespace TheNoobs.Specification.Abstractions;

internal interface ISpecificationComparisonBehavior<TEntity>
{
    SpecificationBehavior Behavior { get; }

    bool Compare(
        TEntity entity,
        ISpecification<TEntity> left,
        ISpecification<TEntity> right,
        out IEnumerable<IIssue> leftIssues,
        out IEnumerable<IIssue> rightIssues);
}

internal interface IOrSpecificationComparisonBehavior<TEntity> : ISpecificationComparisonBehavior<TEntity>
{
}

internal interface IAndSpecificationComparisonBehavior<TEntity> : ISpecificationComparisonBehavior<TEntity>
{
}
