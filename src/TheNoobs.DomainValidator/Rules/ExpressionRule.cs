using System;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions.Rules;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Rules;

public class ExpressionRule<TEntity> : IRule<TEntity>
{
    private readonly Expression<Func<TEntity, bool>> _expression;

    internal ExpressionRule(ValidationResultCode code, ValidationResultDescription description, Expression<Func<TEntity, bool>> expression)
    {
        _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    /// <inheritdoc />
    public ValidationResultCode Code { get; }

    /// <inheritdoc />
    public ValidationResultDescription Description { get; }

    /// <inheritdoc />
    public bool IsSatisfiedBy(TEntity entity)
    {
        return _expression.Compile().Invoke(entity);
    }
}
