using System.Net;
using GlobalExceptionHandler.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GlobalExceptionHandler.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (HandleableException handleableException)
        {
            await HandleExpectedExceptionsAsync(context, handleableException);
        }
        catch (Exception exception)
        {
            await HandleUnhandledExceptionsAsync(context, exception, logger);
        }
    }

    private async Task HandleUnhandledExceptionsAsync(HttpContext context, Exception exception,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        logger.LogError(new EventId(exception.HResult), exception, exception.Message);

        var messageType = ResponseType.Failure;

        var response = new Response(errorCode: (int)messageType, errorMessage: messageType.ToString(),
            HttpStatusCode.InternalServerError);

        await WriteExceptionResponseAsync(context, response, HttpStatusCode.InternalServerError);
    }

    private async Task HandleExpectedExceptionsAsync(HttpContext context, HandleableException handleableException)
    {
        var errorMessage = string.IsNullOrEmpty(handleableException.Message)
            ? handleableException.ResponseType.ToString()
            : handleableException.Message;

        var response = new Response((int?)handleableException.ResponseType, errorMessage,
            handleableException.HttpStatus);

        if (handleableException is ServiceException transactionException)
        {
            response.Result = new { transactionException.OperatorError };
        }

        await WriteExceptionResponseAsync(context, response, handleableException.HttpStatus);
    }

    private Task WriteExceptionResponseAsync(HttpContext context, Response responseData, HttpStatusCode httpStatus)
    {
        var response = context.Response;
        response.StatusCode = (int)httpStatus;
        response.ContentType = ContentTypes.JSON.ToDescription();

        return response.WriteAsync(JsonConvert.SerializeObject(responseData, Formatting.Indented,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }
}