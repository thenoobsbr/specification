using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TheNoobs.DomainValidator.Abstractions.Rules;

namespace TheNoobs.DomainValidator.Rules;

public class IsNotEmptyRuleSpecification<TEntity> : IRuleSpecification<TEntity>
{
    private readonly Expression<Func<TEntity, object>> _propertyExpression;

    public IsNotEmptyRuleSpecification(Expression<Func<TEntity, object>> propertyExpression)
    {
        _propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        var property = _propertyExpression.Compile().Invoke(entity);
        var isNull = property is null;
        if (isNull)
        {
            return false;
        }

        return property switch
        {
            IEnumerable<object> e => e.Any(),
            string s => !string.IsNullOrWhiteSpace(s),
            _ => true
        };
    }
}
