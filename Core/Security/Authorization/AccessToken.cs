namespace Core.Security.Authorization;

public sealed class AccessToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}