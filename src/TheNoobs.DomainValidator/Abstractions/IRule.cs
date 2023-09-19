using TheNoobs.DomainValidator.ValueObjects;

namespace TheNoobs.DomainValidator.Abstractions;

public interface IRule
{
    ValidationResultCode Code { get; }
    ValidationResultDescription Description { get; }
}

public interface IRule<in TEntity> : IRule
{
    bool IsSatisfiedBy(TEntity entity);
}
