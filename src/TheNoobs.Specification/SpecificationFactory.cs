using System;
using System.Linq.Expressions;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification;

public static class SpecificationFactory<TEntity>
{
    public static ISpecificationFactory<TEntity> CircuitBreaker()
    {
        return new CircuitBreakerSpecificationFactory();
    }

    public static ISpecificationFactory<TEntity> NonCircuitBreaker()
    {
        return new NonCircuitBreakerSpecificationFactory();
    }

    private class CircuitBreakerSpecificationFactory : ISpecificationFactory<TEntity>
    {
        public SpecificationBuilder<TEntity> Requires(
            SpecificationCode code,
            SpecificationDescription description,
            Expression<Func<TEntity, bool>> isSatisfiedByExpression)
        {
            return CircuitBreakerSpecificationBuilder<TEntity>.Requires(code, description, isSatisfiedByExpression);
        }

        public SpecificationBuilder<TEntity> Requires(
            SpecificationCode code,
            SpecificationDescription description,
            IRule<TEntity> rule)
        {
            return CircuitBreakerSpecificationBuilder<TEntity>.Requires(code, description, rule);
        }
    }

    private class NonCircuitBreakerSpecificationFactory : ISpecificationFactory<TEntity>
    {
        public SpecificationBuilder<TEntity> Requires(
            SpecificationCode code,
            SpecificationDescription description,
            Expression<Func<TEntity, bool>> isSatisfiedByExpression)
        {
            return NonCircuitBreakerSpecificationBuilder<TEntity>.Requires(code, description, isSatisfiedByExpression);
        }

        public SpecificationBuilder<TEntity> Requires(
            SpecificationCode code,
            SpecificationDescription description,
            IRule<TEntity> rule)
        {
            return NonCircuitBreakerSpecificationBuilder<TEntity>.Requires(code, description, rule);
        }
    }
}
