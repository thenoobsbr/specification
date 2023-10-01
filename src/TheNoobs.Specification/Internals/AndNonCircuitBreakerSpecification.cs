using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal class AndNonCircuitBreakerSpecification<TEntity> : AndSpecification<TEntity>
{
    public AndNonCircuitBreakerSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right) 
        : base(AndSpecificationComparisonBehaviorFactory<TEntity>.Create(SpecificationBehavior.NonCircuitBreaker), left, right)
    {
    }

    public override SpecificationBehavior Behavior => SpecificationBehavior.NonCircuitBreaker;
}

