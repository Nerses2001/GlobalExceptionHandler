namespace GlobalExceptionHandler.Models;

public enum ExceptionErrorType : byte
{
    Api,
    Service,
    Logic,
    Community,
    Repository,
}