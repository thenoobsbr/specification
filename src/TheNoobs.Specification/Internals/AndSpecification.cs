using System;
using System.Collections.Generic;
using System.Linq;
using TheNoobs.Specification.Abstractions;

namespace TheNoobs.Specification.Internals;

internal class AndSpecification<TEntity> : BaseSpecification<TEntity>
{
    private readonly ISpecification<TEntity> _left;
    private readonly ISpecification<TEntity> _right;

    internal AndSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    /// <inheritdoc />
    public override bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues)
    {
        // The validation of both values is done on purpose so that all issues can be obtained at the validation time.
        var leftIsSatisfied = _left.IsSatisfiedBy(entity, out var leftIssues);
        var rightIsSatisfied = _right.IsSatisfiedBy(entity, out var rightIssues);

        if (leftIsSatisfied && rightIsSatisfied)
        {
            issues = Enumerable.Empty<IIssue>();
            return true;
        }
    
        issues = leftIssues.Concat(rightIssues);
        return false;
    }
}
