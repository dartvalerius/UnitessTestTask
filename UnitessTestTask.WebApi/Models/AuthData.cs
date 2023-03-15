using System.ComponentModel.DataAnnotations;

namespace UnitessTestTask.WebApi.Models;

/// <summary>
/// Данные для аутентификации
/// </summary>
public class AuthData
{
    /// <summary>
    /// Логин
    /// </summary>
    [Required(ErrorMessage = "Укажите логин")]
    public string Login { get; set; } = null!;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Укажите пароль")]
    public string Password { get; set; } = null!;
}