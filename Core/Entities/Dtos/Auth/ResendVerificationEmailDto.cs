namespace Core.Entities.Dtos.Auth;

public sealed class ResendVerificationEmailDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}