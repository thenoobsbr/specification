using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal class OrCircuitBreakerSpecification<TEntity> : OrSpecification<TEntity>
{
    public OrCircuitBreakerSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right) 
        : base(OrSpecificationComparisonBehaviorFactory<TEntity>.Create(SpecificationBehavior.CircuitBreaker), left, right)
    {
    }

    public override SpecificationBehavior Behavior => SpecificationBehavior.CircuitBreaker;
}

