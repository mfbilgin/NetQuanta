﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Exceptions.Details;

public sealed class ValidationErrorDetails : IErrorDetails
{
    public IEnumerable<ValidationFailure> ValidationErrors { get; set; } = [];
    public int StatusCode { get; } = StatusCodes.Status422UnprocessableEntity;
    public string Message { get; init; } = "Validation error occurred.";
    public string? RequestedValue { get; init; }

    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}