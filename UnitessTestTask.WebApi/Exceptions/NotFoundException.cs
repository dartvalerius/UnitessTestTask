namespace UnitessTestTask.WebApi.Exceptions;

/// <summary>
/// Исключение отсутствия объекта
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }
}