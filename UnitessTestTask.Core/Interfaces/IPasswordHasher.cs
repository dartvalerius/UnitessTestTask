namespace UnitessTestTask.Core.Interfaces;

/// <summary>
/// Интерфейс хэширования пароля
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Проверить правильность переданного пароля
    /// </summary>
    /// <param name="password">Строка пароля</param>
    /// <param name="hash">Хэш пароля</param>
    /// <param name="salt">Соль шифрования</param>
    /// <returns><c>true</c> - если переданный пароль правильный</returns>
    bool IsValid(string password, string hash, string salt);

    /// <summary>
    /// Получить хэш пароля
    /// </summary>
    /// <param name="password">Строка пароля</param>
    /// <param name="salt">Соль шифрования</param>
    /// <returns>Строка с хэшем пароля</returns>
    string Hash(string password, string salt);

    /// <summary>
    /// Генерировать соль шифрования
    /// </summary>
    /// <returns>Строка с солью шифрования</returns>
    string GenerateSalt();
}