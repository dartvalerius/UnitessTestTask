using System.ComponentModel.DataAnnotations;

namespace UnitessTestTask.WebApi.Models;

/// <summary>
/// Данные для регистрации
/// </summary>
public class RegData
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

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; } = null!;
}