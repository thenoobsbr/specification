namespace TheNoobs.DomainValidator.Abstractions;

public interface IRuleSpecification<in TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
}
