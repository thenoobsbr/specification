using System;
using System.Linq.Expressions;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification;

internal sealed class CircuitBreakerSpecificationBuilder<TEntity> : SpecificationBuilder<TEntity>
{
    private CircuitBreakerSpecificationBuilder(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
        : base(SpecificationBehavior.CircuitBreaker, code, description, isSatisfiedByExpression)
    {
    }

    private CircuitBreakerSpecificationBuilder(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
        : base(SpecificationBehavior.CircuitBreaker, code, description, rule)
    {
    }

    public static SpecificationBuilder<TEntity> Requires(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        return new CircuitBreakerSpecificationBuilder<TEntity>(code, description, isSatisfiedByExpression);
    }

    public static SpecificationBuilder<TEntity> Requires(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        return new CircuitBreakerSpecificationBuilder<TEntity>(code, description, rule);
    }
}
