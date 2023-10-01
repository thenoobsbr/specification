using System.Collections.Generic;

namespace TheNoobs.Specification.Abstractions;

public interface ISpecificationComparisonBehavior<TEntity>
{
    SpecificationBehavior Behavior { get; }

    bool Compare(
        TEntity entity,
        ISpecification<TEntity> left,
        ISpecification<TEntity> right,
        out IEnumerable<IIssue> leftIssues,
        out IEnumerable<IIssue> rightIssues);
}

public interface IOrSpecificationComparisonBehavior<TEntity> : ISpecificationComparisonBehavior<TEntity>
{
}

public interface IAndSpecificationComparisonBehavior<TEntity> : ISpecificationComparisonBehavior<TEntity>
{
}
