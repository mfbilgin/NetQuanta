using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Exceptions.Details;

public sealed class BusinessErrorDetails : IErrorDetails
{
    public int StatusCode { get; init; } = StatusCodes.Status400BadRequest;
    public string Message { get; init; } = "An error occurred.";
    public string? RequestedValue { get; init; }

    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}