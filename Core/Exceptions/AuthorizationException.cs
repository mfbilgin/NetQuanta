namespace Core.Exceptions;

public sealed class AuthorizationException(string? requestedValue) : Exception()
{
    public string? RequestedValue { get; set; } = requestedValue;
}