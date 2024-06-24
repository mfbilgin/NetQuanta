using Core.Exceptions;
using Core.Exceptions.Details;
using Core.Logging;
using Core.Security.Authorization;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions.Exception;

public class ExceptionMiddleware(RequestDelegate next, ILogService logService)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (System.Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, System.Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        IErrorDetails errorDetails = exception switch
        {
            ValidationException validationException => new ValidationErrorDetails
            {
                Message = validationException.Message,
                ValidationErrors = validationException.Errors
            },
            BusinessException businessException => new BusinessErrorDetails
            {
                Message = businessException.Message,
                StatusCode = businessException.StatusCode,
                RequestedValue = businessException.RequestedValue
            },
            AuthorizationException authorizationException => new AuthorizationErrorDetails
            {
                RequestedValue = authorizationException.RequestedValue
            },
            
            // if you are in development mode, you can return the exception message
            _ => new DefaultErrorDetails { Message = exception.Message }

            // else 
            // _ => new DefaultErrorDetails()
        };
        httpContext.Response.StatusCode = errorDetails.StatusCode;

        var stackTrace = exception.StackTrace?.Split("Controllers.")[1].Split("\r")[0];

        Log log = new()
        {
            LogLevel = LogLevel.Error.ToString(),
            Message = errorDetails.Message,
            Exception = exception.GetType().Name,
            Source = stackTrace ?? "Unknown",
            Details = errorDetails.GetDetails(),
            RequestedValue = JwtHelper.GetAuthenticatedUsername() ?? errorDetails.RequestedValue?? "Unknown"
        };

        logService.Log(log);

        return httpContext.Response.WriteAsync(errorDetails.GetDetails());
    }
} 