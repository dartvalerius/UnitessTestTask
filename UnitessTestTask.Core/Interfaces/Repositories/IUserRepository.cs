using UnitessTestTask.Core.Entities;

namespace UnitessTestTask.Core.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    /// <param name="login">Логин</param>
    /// <returns>Пользователь</returns>
    Task<User?> GetByLoginAsync(int login);
}