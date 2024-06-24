using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstracts;

namespace Core.Entities.Concretes;

public class PasswordResetToken : AbstractEntity
{
    [MaxLength(150)]public string Token { get; set; } = string.Empty;
    [MaxLength(50)] public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt => DateTime.Now;
}