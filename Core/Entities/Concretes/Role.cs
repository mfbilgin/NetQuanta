using Core.Entities.Abstracts;

namespace Core.Entities.Concretes;

public sealed class Role : AbstractEntity
{
    public string Name { get; set; } = string.Empty;
}