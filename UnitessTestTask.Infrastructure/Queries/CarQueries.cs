using UnitessTestTask.Core.Entities;

namespace UnitessTestTask.Infrastructure.Queries;

/// <summary>
/// Запросы для работы с таблицей автомобилей
/// </summary>
public class CarQueries
{
    public static string Add =
        $@"insert into CARS (ID, BRAND, LINEUP, COLOR, REG_NUMBER, VIN) VALUES (@{nameof(Car.Id)}, @{nameof(Car.Brand)}, @{nameof(Car.Lineup)}, @{nameof(Car.Color)}, @{nameof(Car.RegNumber)}, @{nameof(Car.Vin)})";

    public static string Update =
        $@"update CARS set BRAND=@{nameof(Car.Brand)}, LINEUP=@{nameof(Car.Lineup)}, COLOR=@{nameof(Car.Color)}, REG_NUMBER=@{nameof(Car.RegNumber)}, VIN=@{nameof(Car.Vin)} where ID=@{nameof(Car.Id)}";

    public static string Delete =
        $@"delete from CARS where ID=@{nameof(Car.Id)}";

    public static string GetAll =
        $@"select ID as {nameof(Car.Id)}, BRAND as {nameof(Car.Brand)}, LINEUP as {nameof(Car.Lineup)}, COLOR as {nameof(Car.Color)}, REG_NUMBER as {nameof(Car.RegNumber)}, VIN as {nameof(Car.Vin)} from CARS";

    public static string GetById =
        $@"select ID as {nameof(Car.Id)}, BRAND as {nameof(Car.Brand)}, LINEUP as {nameof(Car.Lineup)}, COLOR as {nameof(Car.Color)}, REG_NUMBER as {nameof(Car.RegNumber)}, VIN as {nameof(Car.Vin)} from CARS where ID=@Id";

    public static string GetByLimit =
        $@"select ID as {nameof(Car.Id)}, BRAND as {nameof(Car.Brand)}, LINEUP as {nameof(Car.Lineup)}, COLOR as {nameof(Car.Color)}, REG_NUMBER as {nameof(Car.RegNumber)}, VIN as {nameof(Car.Vin)} from CARS limit @Skip,@Count";
}