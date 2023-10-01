using System;
using System.Linq.Expressions;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification;

internal sealed class NonCircuitBreakerSpecificationBuilder<TEntity> : SpecificationBuilder<TEntity>
{
    private NonCircuitBreakerSpecificationBuilder(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
        : base(SpecificationBehavior.NonCircuitBreaker, code, description, isSatisfiedByExpression)
    {
    }

    private NonCircuitBreakerSpecificationBuilder(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
        : base(SpecificationBehavior.NonCircuitBreaker, code, description, rule)
    {
    }


    public static SpecificationBuilder<TEntity> Requires(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        return new NonCircuitBreakerSpecificationBuilder<TEntity>(code, description, isSatisfiedByExpression);
    }

    public static SpecificationBuilder<TEntity> Requires(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        return new NonCircuitBreakerSpecificationBuilder<TEntity>(code, description, rule);
    }
}
