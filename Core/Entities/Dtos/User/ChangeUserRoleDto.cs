namespace Core.Entities.Dtos.User;

public sealed class ChangeUserRoleDto
{
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}