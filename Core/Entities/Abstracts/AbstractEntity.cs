namespace Core.Entities.Abstracts;

public abstract class AbstractEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}