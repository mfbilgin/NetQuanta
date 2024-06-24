using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstracts;

namespace Core.Logging;

public sealed class Log : AbstractEntity
{
    [MaxLength(50)]public string? RequestedValue { get; init; }

    public DateTime TimeStamp { get; init; } = DateTime.Now;
    [MaxLength(15)] public string LogLevel { get; init; } = string.Empty;
    [MaxLength(2000)] public string Message { get; init; } = string.Empty;
    [MaxLength(150)] public string Exception { get; init; } = string.Empty;
    [MaxLength(2000)] public string Source { get; init; } = string.Empty;
    [MaxLength(5000)] public string? Details { get; init; }
}