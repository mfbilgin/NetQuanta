using Core.Exceptions;
using Core.Exceptions.Details;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions.Exception;

public class ExceptionMiddleware(RequestDelegate next)
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

    private static Task HandleExceptionAsync(HttpContext httpContext, System.Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        var stackTrace = exception.StackTrace?.Split("Controllers.")[1].Split("\r")[0];

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
                StatusCode = businessException.StatusCode
            },
            AuthorizationException _ => new AuthorizationErrorDetails(),
            // if you are in development mode, you can return the exception message
            _ => new DefaultErrorDetails { Message = exception.Message }
            // else 
            // _ => new DefaultErrorDetails()
        };
        httpContext.Response.StatusCode = errorDetails.StatusCode;
        // !!! ADD LOGGING HERE !!!

        return httpContext.Response.WriteAsync(errorDetails.GetDetails());
    }
}