using System;
using System.Linq.Expressions;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification.Abstractions;

public interface ISpecificationFactory<TEntity>
{
    SpecificationBuilder<TEntity> Requires(
        SpecificationCode code,
        SpecificationDescription description,
        Expression<Func<TEntity, bool>> isSatisfiedByExpression);

    SpecificationBuilder<TEntity> Requires(
        SpecificationCode code,
        SpecificationDescription description,
        IRule<TEntity> rule);
}
