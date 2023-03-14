namespace UnitessTestTask.Core.Entities;

public class User
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = null!;

    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Соль шифрования
    /// </summary>
    public string Salt { get; set; } = null!;

    /// <summary>
    /// Роли
    /// </summary>
    public IList<string> Roles { get; set; } = null!;
}