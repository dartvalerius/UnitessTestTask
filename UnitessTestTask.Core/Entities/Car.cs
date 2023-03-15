namespace UnitessTestTask.Core.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public class Car
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Марка
    /// </summary>
    public string Brand { get; set; } = null!;

    /// <summary>
    /// Модельный ряд
    /// </summary>
    public string Lineup { get; set; } = null!;

    /// <summary>
    /// Цвет
    /// </summary>
    public string Color { get; set; } = null!;

    /// <summary>
    /// Регистрационный номер
    /// </summary>
    public string RegNumber { get; set; } = null!;

    /// <summary>
    /// Номер кузова
    /// </summary>
    public string Vin { get; set; } = null!;
}