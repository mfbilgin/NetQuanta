namespace Core.Exceptions;

public sealed class BusinessException(string message,int statusCode,string? requestedValue) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
    public string? RequestedValue { get; set; } = requestedValue;
}
