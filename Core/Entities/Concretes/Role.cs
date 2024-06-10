using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstracts;

namespace Core.Entities.Concretes;

public sealed class Role : AbstractEntity
{
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
}