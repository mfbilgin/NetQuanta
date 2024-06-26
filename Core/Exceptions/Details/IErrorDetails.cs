﻿namespace Core.Exceptions.Details;

public interface IErrorDetails
{
    int StatusCode { get; }
    string Message { get; }
    string? RequestedValue { get; }
    string GetDetails();
}