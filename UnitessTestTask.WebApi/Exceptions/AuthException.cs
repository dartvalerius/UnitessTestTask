namespace UnitessTestTask.WebApi.Exceptions;

/// <summary>
/// Исключение аутентификации
/// </summary>
public class AuthException : Exception
{
    public AuthException(string message) : base(message)
    {
        
    }
}