namespace UnitessTestTask.WebApi.Exceptions;

/// <summary>
/// Исключение валидации
/// </summary>
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
        
    }
}