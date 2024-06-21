using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstracts;

namespace Core.Entities.Concretes;

public sealed class EmailVerification : AbstractEntity
{
    [MaxLength(50)] public string Username { get; set; } = string.Empty;
    [MaxLength(81)] public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}