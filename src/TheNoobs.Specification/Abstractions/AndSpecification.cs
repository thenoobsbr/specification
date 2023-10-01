using System;
using System.Collections.Generic;
using System.Linq;
using TheNoobs.Specification.Internals;

namespace TheNoobs.Specification.Abstractions;

internal abstract class AndSpecification<TEntity> : BaseSpecification<TEntity>
{
    private readonly IAndSpecificationComparisonBehavior<TEntity> _comparisonBehavior;
    private readonly ISpecification<TEntity> _left;
    private readonly ISpecification<TEntity> _right;

    protected AndSpecification(IAndSpecificationComparisonBehavior<TEntity> comparisonBehavior, ISpecification<TEntity> left, ISpecification<TEntity> right)
    {
        _comparisonBehavior = comparisonBehavior ?? throw new ArgumentNullException(nameof(comparisonBehavior));
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    /// <inheritdoc />
    public override bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues)
    {
        var isSatisfied = _comparisonBehavior.Compare(
            entity, 
            _left, 
            _right, 
            out var leftIssues, 
            out var rightIssues);

        if (isSatisfied)
        {
            issues = Enumerable.Empty<IIssue>();
            return true;
        }

        issues = leftIssues.Concat(rightIssues);
        return false;
    }
}
