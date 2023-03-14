namespace JwtAuthentication.AspNetCore;

/// <summary>
/// Настройки JWT токена
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Издатель токена
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Получатель
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Ключ безопасности
    /// </summary>
    public string SecurityKey { get; set; } = null!;

    /// <summary>
    /// Количество дней срока действия
    /// </summary>
    public double ExpirationDays { get; set; }
}