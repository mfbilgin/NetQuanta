namespace Core.Exceptions;

public sealed class BusinessException(string message,int statusCode) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}
