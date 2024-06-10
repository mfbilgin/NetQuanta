using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstracts;

namespace Core.Entities.Concretes;

public sealed class User : AbstractEntity
{
    public Guid RoleId { get; set; }
    [MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [MaxLength(75)] public string LastName { get; set; } = string.Empty;
    [MaxLength(150)] public string Email { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; } = false;
    [MaxLength(50)] public string Username { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}