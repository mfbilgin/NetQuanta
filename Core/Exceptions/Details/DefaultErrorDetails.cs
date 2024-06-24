using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Exceptions.Details;

public sealed class DefaultErrorDetails : IErrorDetails
{
    public int StatusCode { get; } = StatusCodes.Status500InternalServerError;

    public string Message { get; init; } = "An error occurred.";
    public string? RequestedValue { get; init; }

    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}