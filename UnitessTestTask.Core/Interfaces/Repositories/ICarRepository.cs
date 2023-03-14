using UnitessTestTask.Core.Entities;

namespace UnitessTestTask.Core.Interfaces.Repositories;

public interface ICarRepository : IGenericRepository<Car>
{
    /// <summary>
    /// Получить список автомобилей по заданным условиям
    /// </summary>
    /// <param name="skip">Сколько пропустить</param>
    /// <param name="count">Сколько вывести</param>
    /// <returns>Список автомобилей</returns>
    Task<IReadOnlyList<Car>> GetByLimitAsync(int skip, int count);
}