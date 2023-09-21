using System.Collections.Generic;
using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal abstract class BaseSpecification<TEntity> : ICompositeSpecification<TEntity>
{
    /// <inheritdoc />
    public ICompositeSpecification<TEntity> And(ISpecification<TEntity> other)
    {
        return new AndSpecification<TEntity>(this, other);
    }

    /// <inheritdoc />
    public abstract bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues);

    /// <inheritdoc />
    public ICompositeSpecification<TEntity> Or(ISpecification<TEntity> other)
    {
        return new OrSpecification<TEntity>(this, other);
    }
}
