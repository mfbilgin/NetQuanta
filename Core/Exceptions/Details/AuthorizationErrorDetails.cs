using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Exceptions.Details;

public class AuthorizationErrorDetails : IErrorDetails
{
    public int StatusCode { get; } = StatusCodes.Status401Unauthorized;
    public string Message { get; } = "Bu alana erişmek için yetkin yok. Bir sorun olduğunu düşünüyorsan bizimle iletişime geçebilirsin.";
    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}