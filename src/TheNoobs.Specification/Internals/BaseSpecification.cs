using System.Collections.Generic;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.Exceptions;

namespace TheNoobs.Specification.Internals;

internal abstract class BaseSpecification<TEntity> : ICompositeSpecification<TEntity>
{
    /// <inheritdoc />
    public ICompositeSpecification<TEntity> And(ISpecification<TEntity> other)
    {
        return Behavior switch
        {
            SpecificationBehavior.CircuitBreaker => new AndCircuitBreakerSpecification<TEntity>(this, other),
            SpecificationBehavior.NonCircuitBreaker => new AndNonCircuitBreakerSpecification<TEntity>(this, other),
            _ => throw new SpecificationBehaviorNotSupportedException(Behavior)
        };
    }

    public abstract SpecificationBehavior Behavior { get; }

    /// <inheritdoc />
    public abstract bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues);

    /// <inheritdoc />
    public ICompositeSpecification<TEntity> Or(ISpecification<TEntity> other)
    {
        return Behavior switch
        {
            SpecificationBehavior.CircuitBreaker => new OrCircuitBreakerSpecification<TEntity>(this, other),
            SpecificationBehavior.NonCircuitBreaker => new OrNonCircuitBreakerSpecification<TEntity>(this, other),
            _ => throw new SpecificationBehaviorNotSupportedException(Behavior)
        };
    }
}
