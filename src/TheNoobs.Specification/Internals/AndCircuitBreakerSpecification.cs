using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal class AndCircuitBreakerSpecification<TEntity> : AndSpecification<TEntity>
{
    public AndCircuitBreakerSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right) 
        : base(AndSpecificationComparisonBehaviorFactory<TEntity>.Create(SpecificationBehavior.CircuitBreaker), left, right)
    {
    }

    public override SpecificationBehavior Behavior => SpecificationBehavior.CircuitBreaker;
}

