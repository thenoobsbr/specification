using System;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions.Rules;

namespace TheNoobs.DomainValidator.Rules;

public class IsNotNullRuleSpecification<TEntity> : IRuleSpecification<TEntity>
{
    private readonly Expression<Func<TEntity, object?>> _propertyExpression;

    public IsNotNullRuleSpecification(Expression<Func<TEntity, object?>> propertyExpression)
    {
        _propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        var property = _propertyExpression.Compile().Invoke(entity);
        return property is not null;
    }
}
