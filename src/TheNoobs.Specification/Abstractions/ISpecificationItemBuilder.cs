using TheNoobs.Specification.ValueObjects;

namespace TheNoobs.Specification.Abstractions;

public interface ISpecificationItemBuilder<TEntity>
{
    SpecificationBuilder<TEntity> WithCodeAndDescription(SpecificationCode code, SpecificationDescription description);
}
