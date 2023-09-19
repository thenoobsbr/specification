namespace TheNoobs.DomainValidator.Abstractions.Rules;

public interface IRuleSpecification<in TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
}
