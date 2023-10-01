using System;
using System.Linq.Expressions;
using TheNoobs.Specification.Internals;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification.Abstractions;

public abstract class SpecificationBuilder<TEntity>
{
    private readonly SpecificationBehavior _behavior;
    private ICompositeSpecification<TEntity> _specification;

    protected SpecificationBuilder(SpecificationBehavior behavior, SpecificationCode code, SpecificationDescription description,
        Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        _behavior = behavior;
        _specification = new ExpressionSpecification<TEntity>(_behavior, code, description, isSatisfiedByExpression);
    }

    protected SpecificationBuilder(SpecificationBehavior behavior, SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        _behavior = behavior;
        _specification = new RuleSpecification<TEntity>(_behavior, code, description, rule);
    }

    // public ISpecificationItemBuilder<TEntity> Requires(Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    // {
    //     return new InitialExpressionSpecificationItemBuilder(isSatisfiedByExpression);
    // }

    // public static ISpecificationItemBuilder<TEntity> Requires(IRule<TEntity> rule)
    // {
    //     return new InitialRuleSpecificationItemBuilder(rule);
    // }

    public SpecificationBuilder<TEntity> And(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        _specification = _specification.And(new ExpressionSpecification<TEntity>(_behavior, code, description, isSatisfiedByExpression));
        return this;
    }

    public ISpecificationItemBuilder<TEntity> And(Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        return new AndExpressionSpecificationItemBuilder(this, isSatisfiedByExpression);
    }

    public ISpecificationItemBuilder<TEntity> And(IRule<TEntity> rule)
    {
        return new AndRuleSpecificationItemBuilder(this, rule);
    }

    public SpecificationBuilder<TEntity> And(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        _specification = _specification.And(new RuleSpecification<TEntity>(_behavior, code, description, rule));
        return this;
    }

    public ISpecification<TEntity> Build()
    {
        return _specification;
    }

    public SpecificationBuilder<TEntity> Or(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        _specification = _specification.Or(new ExpressionSpecification<TEntity>(_behavior, code, description, isSatisfiedByExpression));
        return this;
    }

    public ISpecificationItemBuilder<TEntity> Or(Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        return new OrExpressionSpecificationItemBuilder(this, isSatisfiedByExpression);
    }

    public ISpecificationItemBuilder<TEntity> Or(IRule<TEntity> rule)
    {
        return new OrRuleSpecificationItemBuilder(this, rule);
    }

    public SpecificationBuilder<TEntity> Or(SpecificationCode code, SpecificationDescription description, IRule<TEntity> rule)
    {
        _specification = _specification.Or(new RuleSpecification<TEntity>(_behavior, code, description, rule));
        return this;
    }

    private sealed class AndExpressionSpecificationItemBuilder : ISpecificationItemBuilder<TEntity>
    {
        private readonly Expression<Func<TEntity, bool>> _isSatisfiedByExpression;
        private readonly SpecificationBuilder<TEntity> _specificationBuilder;

        public AndExpressionSpecificationItemBuilder(SpecificationBuilder<TEntity> specificationBuilder, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
        {
            _specificationBuilder = specificationBuilder ?? throw new ArgumentNullException(nameof(specificationBuilder));
            _isSatisfiedByExpression = isSatisfiedByExpression ?? throw new ArgumentNullException(nameof(isSatisfiedByExpression));
        }

        public SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description)
        {
            return _specificationBuilder.And(code, description, _isSatisfiedByExpression);
        }
    }

    private sealed class AndRuleSpecificationItemBuilder : ISpecificationItemBuilder<TEntity>
    {
        private readonly IRule<TEntity> _rule;
        private readonly SpecificationBuilder<TEntity> _specificationBuilder;

        public AndRuleSpecificationItemBuilder(SpecificationBuilder<TEntity> specificationBuilder, IRule<TEntity> rule)
        {
            _specificationBuilder = specificationBuilder ?? throw new ArgumentNullException(nameof(specificationBuilder));
            _rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }

        public SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description)
        {
            return _specificationBuilder.And(code, description, _rule);
        }
    }

    // private sealed class InitialExpressionSpecificationItemBuilder : ISpecificationItemBuilder<TEntity>
    // {
    //     private readonly Expression<Func<TEntity, bool>> _isSatisfiedByExpression;
    //
    //     public InitialExpressionSpecificationItemBuilder(Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    //     {
    //         _isSatisfiedByExpression = isSatisfiedByExpression ?? throw new ArgumentNullException(nameof(isSatisfiedByExpression));
    //     }
    //
    //     public SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description)
    //     {
    //         return Requires(code, description, _isSatisfiedByExpression);
    //     }
    // }

    // private sealed class InitialRuleSpecificationItemBuilder : ISpecificationItemBuilder<TEntity>
    // {
    //     private readonly IRule<TEntity> _rule;
    //
    //     public InitialRuleSpecificationItemBuilder(IRule<TEntity> rule)
    //     {
    //         _rule = rule ?? throw new ArgumentNullException(nameof(rule));
    //     }
    //
    //     public SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description)
    //     {
    //         return Requires(code, description, _rule);
    //     }
    // }

    private sealed class OrExpressionSpecificationItemBuilder : ISpecificationItemBuilder<TEntity>
    {
        private readonly Expression<Func<TEntity, bool>> _isSatisfiedByExpression;
        private readonly SpecificationBuilder<TEntity> _specificationBuilder;

        public OrExpressionSpecificationItemBuilder(SpecificationBuilder<TEntity> specificationBuilder, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
        {
            _specificationBuilder = specificationBuilder ?? throw new ArgumentNullException(nameof(specificationBuilder));
            _isSatisfiedByExpression = isSatisfiedByExpression ?? throw new ArgumentNullException(nameof(isSatisfiedByExpression));
        }

        public SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description)
        {
            return _specificationBuilder.Or(code, description, _isSatisfiedByExpression);
        }
    }

    private sealed class OrRuleSpecificationItemBuilder : ISpecificationItemBuilder<TEntity>
    {
        private readonly IRule<TEntity> _rule;
        private readonly SpecificationBuilder<TEntity> _specificationBuilder;

        public OrRuleSpecificationItemBuilder(SpecificationBuilder<TEntity> specificationBuilder, IRule<TEntity> rule)
        {
            _specificationBuilder = specificationBuilder ?? throw new ArgumentNullException(nameof(specificationBuilder));
            _rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }

        public SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description)
        {
            return _specificationBuilder.Or(code, description, _rule);
        }
    }
}
