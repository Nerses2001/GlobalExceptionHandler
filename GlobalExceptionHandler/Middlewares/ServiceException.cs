using System.Net;
using GlobalExceptionHandler.Models;

namespace GlobalExceptionHandler.Middlewares;

public abstract class ServiceException : HandleableException
{
    public string OperatorError { get; }

    public ServiceException(string message, ResponseType responseType, HttpStatusCode httpStatus,string operatorError): base(message, (int)responseType, httpStatus)
    {
        OperatorError = operatorError;
    }
}