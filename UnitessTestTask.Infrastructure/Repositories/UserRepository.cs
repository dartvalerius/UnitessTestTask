using Dapper;
using UnitessTestTask.Core.Entities;
using UnitessTestTask.Core.Interfaces.Repositories;
using UnitessTestTask.Infrastructure.Database;
using UnitessTestTask.Infrastructure.Queries;

namespace UnitessTestTask.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbContext _dbContext;

    public UserRepository(DbContext dbContext) => _dbContext = dbContext;

    public async Task<User?> GetByIdAsync(string id)
    {
        using var connection = _dbContext.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.GetById, new { Id = id });

        return result;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        using var connection = _dbContext.CreateConnection();

        var result = await connection.QueryAsync<User>(UserQueries.GetAll);

        return result.ToList();
    }

    public async Task AddAsync(User user)
    {
        using var connection = _dbContext.CreateConnection();

        await connection.ExecuteAsync(UserQueries.Add, user);
    }

    public async Task UpdateAsync(User user)
    {
        using var connection = _dbContext.CreateConnection();

        await connection.ExecuteAsync(UserQueries.Update, user);
    }

    public async Task DeleteAsync(string id)
    {
        using var connection = _dbContext.CreateConnection();

        await connection.ExecuteAsync(UserQueries.Delete, new { Id = id });
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        using var connection = _dbContext.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.GetByLogin, new { Login = login });

        return result;
    }
}