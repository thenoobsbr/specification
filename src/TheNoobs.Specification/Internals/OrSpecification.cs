using System;
using System.Collections.Generic;
using System.Linq;
using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal class OrSpecification<TEntity> : BaseSpecification<TEntity>
{
    private readonly ISpecification<TEntity> _left;
    private readonly ISpecification<TEntity> _right;

    internal OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    /// <inheritdoc />
    public override bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues)
    {
        var isSatisfied = 
            _left.IsSatisfiedBy(entity, out var leftIssues)
            | _right.IsSatisfiedBy(entity, out var rightIssues);

        if (isSatisfied)
        {
            issues = Enumerable.Empty<IIssue>();
            return true;
        }
    
        issues = leftIssues.Concat(rightIssues);
        return false;
    }
}
