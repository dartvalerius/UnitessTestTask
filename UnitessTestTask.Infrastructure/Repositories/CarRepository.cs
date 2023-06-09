﻿using Dapper;
using UnitessTestTask.Core.Entities;
using UnitessTestTask.Core.Interfaces.Repositories;
using UnitessTestTask.Infrastructure.Database;
using UnitessTestTask.Infrastructure.Queries;

namespace UnitessTestTask.Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DbContext _dbContext;

    public CarRepository(DbContext dbContext) => _dbContext = dbContext;

    public async Task<Car?> GetByIdAsync(string id)
    {
        using var connection = _dbContext.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<Car>(CarQueries.GetById, new { Id = id });

        return result;
    }

    public async Task<IReadOnlyList<Car>> GetAllAsync()
    {
        using var connection = _dbContext.CreateConnection();

        var result = await connection.QueryAsync<Car>(CarQueries.GetAll);

        return result.ToList();
    }

    public async Task<int> AddAsync(Car car)
    {
        using var connection = _dbContext.CreateConnection();

        return await connection.ExecuteAsync(CarQueries.Add, car);
    }

    public async Task<int> UpdateAsync(Car car)
    {
        using var connection = _dbContext.CreateConnection();

        return await connection.ExecuteAsync(CarQueries.Update, car);
    }

    public async Task<int> DeleteAsync(string id)
    {
        using var connection = _dbContext.CreateConnection();

        return await connection.ExecuteAsync(CarQueries.Delete, new { Id = id });
    }

    public async Task<IReadOnlyList<Car>> GetByLimitAsync(int skip, int count)
    {
        using var connection = _dbContext.CreateConnection();

        var result = await connection.QueryAsync<Car>(CarQueries.GetByLimit, new { Skip = skip, Count = count });

        return result.ToList();
    }
}