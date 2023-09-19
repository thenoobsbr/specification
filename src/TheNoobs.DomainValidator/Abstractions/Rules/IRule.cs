using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions.Rules;

public interface IRule
{
    ValidationResultCode Code { get; }
    ValidationResultDescription Description { get; }
}

public interface IRule<in TEntity> : IRule
{
    /// <summary>
    ///     Checks if the rule is satisfied by the entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if rule is satisfied, otherwise false.</returns>
    bool IsSatisfiedBy(TEntity entity);
}
