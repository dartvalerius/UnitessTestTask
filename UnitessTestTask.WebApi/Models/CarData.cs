using System.ComponentModel.DataAnnotations;

namespace UnitessTestTask.WebApi.Models;

/// <summary>
/// Данные автомобиля
/// </summary>
public class CarData
{
    /// <summary>
    /// Марка
    /// </summary>
    [Required(ErrorMessage = "Укажите марку")]
    public string Brand { get; set; } = null!;

    /// <summary>
    /// Модельный ряд
    /// </summary>
    [Required(ErrorMessage = "Укажите модельный ряд")]
    public string Lineup { get; set; } = null!;

    /// <summary>
    /// Цвет
    /// </summary>
    [Required(ErrorMessage = "Укажите цвет")]
    public string Color { get; set; } = null!;

    /// <summary>
    /// Регистрационный номер
    /// </summary>
    [Required(ErrorMessage = "Укажите регистрационный номер")]
    public string RegNumber { get; set; } = null!;

    /// <summary>
    /// Номер кузова
    /// </summary>
    [Required(ErrorMessage = "Укажите номер кузова")] 
    public string Vin { get; set; } = null!;
}