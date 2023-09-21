namespace TheNoobs.Specification.Abstractions;

public interface IRule<in TEntity>
{
    /// <summary>
    ///     Checks if the rule is satisfied by the entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool IsSatisfiedBy(TEntity entity);
}
