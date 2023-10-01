using System;
using System.Collections.Generic;
using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

public static class OrSpecificationComparisonBehaviorFactory<TEntity>
{
    public static IOrSpecificationComparisonBehavior<TEntity> Create(SpecificationBehavior behavior)
    {
        return behavior switch
        {
            SpecificationBehavior.CircuitBreaker => new OrCircuitBreakerComparisonBehavior(),
            SpecificationBehavior.NonCircuitBreaker => new OrNonCircuitBreakerComparisonBehavior(),
            _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null)
        };
    }

    private class OrCircuitBreakerComparisonBehavior : IOrSpecificationComparisonBehavior<TEntity>
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
                   || right.IsSatisfiedBy(entity, out rightIssues);
        }
    }

    private class OrNonCircuitBreakerComparisonBehavior : IOrSpecificationComparisonBehavior<TEntity>
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
                   | right.IsSatisfiedBy(entity, out rightIssues);
        }
    }
}
