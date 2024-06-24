using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Exceptions.Details;

public sealed class AuthorizationErrorDetails : IErrorDetails
{
    public int StatusCode { get; } = StatusCodes.Status401Unauthorized;
    public string Message { get; } = "Bu alana erişmek için yetkin yok. Bir sorun olduğunu düşünüyorsan bizimle iletişime geçebilir ya da yeniden giriş yapmayı deneyebilirsin.";
    public string? RequestedValue { get; set; }
    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}