using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification.Internals;

internal class ExpressionSpecification<TEntity> : BaseSpecification<TEntity>
{
    private readonly Expression<Func<TEntity, bool>> _isSatisfiedByExpression;

    public ExpressionSpecification(SpecificationCode code, SpecificationDescription description, Expression<Func<TEntity, bool>> isSatisfiedByExpression)
    {
        _isSatisfiedByExpression = isSatisfiedByExpression ?? throw new ArgumentNullException(nameof(isSatisfiedByExpression));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public SpecificationCode Code { get; }

    public SpecificationDescription Description { get; }

    /// <inheritdoc />
    public override bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues)
    {
        var isSatisfied = _isSatisfiedByExpression.Compile().Invoke(entity);
        issues = isSatisfied ? Enumerable.Empty<IIssue>() : new[] { new Issue(this) };
        return isSatisfied;
    }

    private class Issue : IIssue
    {
        private readonly ExpressionSpecification<TEntity> _specification;

        public Issue(ExpressionSpecification<TEntity> specification)
        {
            _specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        public SpecificationCode Code => _specification.Code;
        public SpecificationDescription Description => _specification.Description;
    }
}
