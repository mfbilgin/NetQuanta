namespace Core.Entities.Dtos.User;

public sealed class ChangePasswordDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}