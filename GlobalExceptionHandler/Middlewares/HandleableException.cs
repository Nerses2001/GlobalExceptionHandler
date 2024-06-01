using System.Net;
using GlobalExceptionHandler.Models;

namespace GlobalExceptionHandler.Middlewares;

public class HandleableException : Exception
{
    public ResponseType? ResponseType { get; }

    public HttpStatusCode HttpStatus { get; set; }

    protected HandleableException(string message, HttpStatusCode statusCode) : base(message)
    {
        HttpStatus = statusCode;
    }

    protected HandleableException(ResponseType message, HttpStatusCode statusCode) : base(message.ToString())
    {
        ResponseType = message;
        HResult = (int)message;
        HttpStatus = statusCode;
    }

    protected HandleableException(ResponseType errorCode, string message, HttpStatusCode statusCode) : base(message)
    {
        ResponseType = errorCode;
        HttpStatus = statusCode;
        HResult = (int)errorCode;
    }

    protected HandleableException(string message, int errorCode, HttpStatusCode statusCode) : base(message)
    {
        HttpStatus = statusCode;
        HResult = errorCode;
    }

    public HandleableException()
    {
    }

    public HandleableException(string message) : base(message)
    {
    }

    public HandleableException(string message, System.Exception innerException) : base(message, innerException)
    {
    }
}