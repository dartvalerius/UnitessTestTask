using UnitessTestTask.Core.Entities;

namespace UnitessTestTask.Infrastructure.Queries;

/// <summary>
/// Запросы для работы с таблицей пользователей
/// </summary>
public static class UserQueries
{
    public static string Add =
        $@"insert into USERS (ID, LOGIN, PASSWORD, SALT, ROLE) VALUES (@{nameof(User.Id)}, @{nameof(User.Login)}, @{nameof(User.Password)}, @{nameof(User.Salt)}, @{nameof(User.Role)})";

    public static string Update =
        $@"update USERS set LOGIN=@{nameof(User.Login)}, PASSWORD=@{nameof(User.Password)}, SALT=@{nameof(User.Salt)}, ROLE=@{nameof(User.Role)} where ID=@{nameof(User.Id)}";

    public static string Delete =
        $@"delete from USERS where ID=@Id";

    public static string GetAll =
        $@"select ID as {nameof(User.Id)}, LOGIN as {nameof(User.Login)}, PASSWORD as {nameof(User.Password)}, SALT as {nameof(User.Salt)}, ROLE as {nameof(User.Role)} from USERS";

    public static string GetById =
        $@"select ID as {nameof(User.Id)}, LOGIN as {nameof(User.Login)}, PASSWORD as {nameof(User.Password)}, SALT as {nameof(User.Salt)}, ROLE as {nameof(User.Role)} from USERS where ID=@Id";

    public static string GetByLogin =
        $@"select ID as {nameof(User.Id)}, LOGIN as {nameof(User.Login)}, PASSWORD as {nameof(User.Password)}, SALT as {nameof(User.Salt)}, ROLE as {nameof(User.Role)} from USERS where LOGIN=@Login";
}