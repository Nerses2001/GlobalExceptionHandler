using System.Net;

namespace GlobalExceptionHandler.Models;

public class Response(int? errorCode, string errorMessage, HttpStatusCode httpStatusCode)
{
    public int? ErrorCode { get; set; } = errorCode;
    public string ErrorMessage { get; set; } = errorMessage;
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
    public object? Result { get; set; } = null;
}