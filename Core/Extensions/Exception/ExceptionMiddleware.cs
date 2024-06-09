using Core.Exceptions;
using Core.Exceptions.Details;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions.Exception;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private IEnumerable<ValidationFailure> _errors;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (System.Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, System.Exception exception)
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
                StatusCode = businessException.StatusCode
            },
            AuthorizationException _ => new AuthorizationErrorDetails(),
            _ => new DefaultErrorDetails()
        };

        httpContext.Response.StatusCode = errorDetails.StatusCode;
        return httpContext.Response.WriteAsync(errorDetails.GetDetails());
    }
}