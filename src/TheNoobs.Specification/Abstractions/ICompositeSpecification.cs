namespace TheNoobs.Specification.Abstractions;

public interface ICompositeSpecification<TEntity> : ISpecification<TEntity>
{
    SpecificationBehavior Behavior { get; }
    
    /// <summary>
    ///     Combines the specification with another using the logical AND operator.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    ICompositeSpecification<TEntity> And(ISpecification<TEntity> other);


    /// <summary>
    ///     Combines the specification with another using the logical OR operator.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    ICompositeSpecification<TEntity> Or(ISpecification<TEntity> other);
}
