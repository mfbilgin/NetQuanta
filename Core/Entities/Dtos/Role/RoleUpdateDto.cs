namespace Core.Entities.Dtos.Role;

public sealed class RoleUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}