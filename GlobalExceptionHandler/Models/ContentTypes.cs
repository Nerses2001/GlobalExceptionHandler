using System.ComponentModel;

namespace GlobalExceptionHandler.Models;

public enum ContentTypes : byte
{
    [Description("application/json")]
    JSON,
}