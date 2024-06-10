namespace Core.Entities.Dtos.Role;

public sealed class RoleGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}