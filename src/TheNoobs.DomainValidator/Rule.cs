using System;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions;
using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator;

public class Rule<TEntity> : IRule<TEntity>
{
    private readonly Expression<Func<TEntity, bool>> _expression;

    internal Rule(ValidationResultCode code, ValidationResultDescription description, Expression<Func<TEntity, bool>> expression)
    {
        _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public ValidationResultCode Code { get; }
    public ValidationResultDescription Description { get; }

    public bool IsSatisfied(TEntity entity)
    {
        return _expression.Compile().Invoke(entity);
    }
}
