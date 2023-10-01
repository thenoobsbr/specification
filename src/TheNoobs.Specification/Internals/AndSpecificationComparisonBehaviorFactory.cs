using System;
using System.Collections.Generic;
using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

public static class AndSpecificationComparisonBehaviorFactory<TEntity>
{
    public static IAndSpecificationComparisonBehavior<TEntity> Create(SpecificationBehavior behavior)
    {
        return behavior switch
        {
            SpecificationBehavior.CircuitBreaker => new AndCircuitBreakerComparisonBehavior(),
            SpecificationBehavior.NonCircuitBreaker => new AndNonCircuitBreakerComparisonBehavior(),
            _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null)
        };
    }

    private class AndCircuitBreakerComparisonBehavior : IAndSpecificationComparisonBehavior<TEntity>
    {
        public SpecificationBehavior Behavior => SpecificationBehavior.CircuitBreaker;

        public bool Compare(
            TEntity entity,
            ISpecification<TEntity> left,
            ISpecification<TEntity> right,
            out IEnumerable<IIssue> leftIssues,
            out IEnumerable<IIssue> rightIssues)
        {
            rightIssues = Array.Empty<IIssue>();
            return left.IsSatisfiedBy(entity, out leftIssues)
                   && right.IsSatisfiedBy(entity, out rightIssues);
        }
    }

    private class AndNonCircuitBreakerComparisonBehavior : IAndSpecificationComparisonBehavior<TEntity>
    {
        public SpecificationBehavior Behavior => SpecificationBehavior.NonCircuitBreaker;

        public bool Compare(
            TEntity entity,
            ISpecification<TEntity> left,
            ISpecification<TEntity> right,
            out IEnumerable<IIssue> leftIssues,
            out IEnumerable<IIssue> rightIssues)
        {
            return left.IsSatisfiedBy(entity, out leftIssues)
                   & right.IsSatisfiedBy(entity, out rightIssues);
        }
    }
}
