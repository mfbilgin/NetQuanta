using Core.Entities.Abstracts;

namespace Core.Entities.Concretes;

public class PasswordResetToken : AbstractEntity
{
    public string Token { get; set; } = string.Empty;
}