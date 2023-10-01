using System;
using System.Collections.Generic;
using System.Linq;
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification.Internals;

internal class RuleSpecification<TEntity> : BaseSpecification<TEntity>
{
    private readonly IRule<TEntity> _rule;

    public RuleSpecification(
        SpecificationBehavior behavior,
        SpecificationCode code,
        SpecificationDescription description,
        IRule<TEntity> rule)
    {
        _rule = rule ?? throw new ArgumentNullException(nameof(rule));
        Behavior = behavior;
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public override SpecificationBehavior Behavior { get; }

    public SpecificationCode Code { get; }

    public SpecificationDescription Description { get; }

    /// <inheritdoc />
    public override bool IsSatisfiedBy(TEntity entity, out IEnumerable<IIssue> issues)
    {
        var isSatisfied = _rule.IsSatisfiedBy(entity);
        issues = isSatisfied ? Enumerable.Empty<IIssue>() : new[] { new Issue(this) };
        return isSatisfied;
    }

    private class Issue : IIssue
    {
        private readonly RuleSpecification<TEntity> _specification;

        public Issue(RuleSpecification<TEntity> specification)
        {
            _specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        public SpecificationCode Code => _specification.Code;
        public SpecificationDescription Description => _specification.Description;
    }
}
