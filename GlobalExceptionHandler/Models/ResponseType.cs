namespace GlobalExceptionHandler.Models;

public enum ResponseType: byte
{
    Success,
    Failure,
    Retry,
    NotFound,
    Unauthorized,
}