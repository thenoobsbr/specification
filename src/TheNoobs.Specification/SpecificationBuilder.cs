using System;
using System.Linq.Expressions;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.Internals;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification;

public sealed class SpecificationBuilder<TEntity>
{
    private ICompositeSpecification<TEntity> _specification;

    private SpecificationBuilder(ICompositeSpecification<TEntity> specification)
    {
        _specification = specification ?? throw new ArgumentNullException(nameof(specification));
    }

    public static SpecificationBuilder<TEntity> Requires(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        return new SpecificationBuilder<TEntity>(new ExpressionSpecification<TEntity>(code, description, isSatisfiedByExpression));
    }

    public static SpecificationBuilder<TEntity> Requires(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        return new SpecificationBuilder<TEntity>(new RuleSpecification<TEntity>(code, description, rule));
    }

    public SpecificationBuilder<TEntity> And(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        _specification = new AndSpecification<TEntity>(_specification, new ExpressionSpecification<TEntity>(code, description, isSatisfiedByExpression));
        return this;
    }

    public SpecificationBuilder<TEntity> And(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        _specification = new AndSpecification<TEntity>(_specification, new RuleSpecification<TEntity>(code, description, rule));
        return this;
    }

    public SpecificationBuilder<TEntity> Or(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        _specification = new OrSpecification<TEntity>(_specification, new ExpressionSpecification<TEntity>(code, description, isSatisfiedByExpression));
        return this;
    }

    public SpecificationBuilder<TEntity> Or(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        _specification = new OrSpecification<TEntity>(_specification, new RuleSpecification<TEntity>(code, description, rule));
        return this;
    }
    
    public ISpecification<TEntity> Build()
    {
        return _specification;
    }
}
