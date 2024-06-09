namespace Core.Exceptions;

public class BusinessException(string message,int statusCode) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}
