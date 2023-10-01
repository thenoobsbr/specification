using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal class OrNonCircuitBreakerSpecification<TEntity> : OrSpecification<TEntity>
{
    public OrNonCircuitBreakerSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        : base(OrSpecificationComparisonBehaviorFactory<TEntity>.Create(SpecificationBehavior.NonCircuitBreaker), left, right)
    {
    }

    public override SpecificationBehavior Behavior => SpecificationBehavior.NonCircuitBreaker;
}
