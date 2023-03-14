using System.Data;
using Microsoft.Data.Sqlite;

namespace UnitessTestTask.Infrastructure.Database;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}