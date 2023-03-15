using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnitessTestTask.Core.Entities;
using UnitessTestTask.Core.Interfaces.Repositories;
using UnitessTestTask.WebApi.Controllers.Abstract;
using UnitessTestTask.WebApi.Exceptions;
using UnitessTestTask.WebApi.Models;

namespace UnitessTestTask.WebApi.Controllers;

/// <summary>
/// Работа с данными автомобилей
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Authorize]
public class CarController : BaseController
{
    private readonly ICarRepository _carRepository;

    public CarController(ICarRepository carRepository) => _carRepository = carRepository;

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <response code="200">Успех</response>
    /// <response code="401">Клиент не авторизован</response>
    /// <response code="403">Доступ запрещён</response>
    /// <response code="404">Объект с переданным идентификатором не найден</response>
    /// <response code="500">Произошла ошибка на сервере</response>
    [HttpGet("get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Car))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Car>> GetByIdAsync(string id)
    {
        var car = await _carRepository.GetByIdAsync(id);

        if (car == null) throw new NotFoundException("Автомобиль не найден");

        return Ok(car);
    }

    /// <summary>
    /// Получить список всех автомобилей
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="401">Клиент не авторизован</response>
    /// <response code="403">Доступ запрещён</response>
    /// <response code="500">Произошла ошибка на сервере</response>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Car>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<IReadOnlyList<Car>>> GetAllAsync() => Ok(await _carRepository.GetAllAsync());

    /// <summary>
    /// Добавить новый автомобиль
    /// </summary>
    /// <param name="carData">Данные автомобиля</param>
    /// <response code="201">Объект создан</response>
    /// <response code="400">Передан не корректный объект</response>
    /// <response code="401">Клиент не авторизован</response>
    /// <response code="403">Доступ запрещён</response>
    /// <response code="500">Произошла ошибка на сервере</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Car))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Car>> CreateCar([FromBody] CarData carData)
    {
        if (carData == null) throw new ValidationException("Передан пустой объект");
        CheckModelState();

        //TODO: Проверка на наличие у других авто переданного регистрационного номера и VIN

        var car = ConvertToModel(carData);

        car.Id = Guid.NewGuid().ToString();

        var result = await _carRepository.AddAsync(car);

        if (result == 0) throw new Exception("Объект не добавлен");

        return Created("get", car);
    }

    /// <summary>
    /// Изменить данные автомобиля
    /// </summary>
    /// <param name="carData">Данные автомобиля</param>
    /// <param name="id">Идентификатор</param>
    /// <response code="200">Успех</response>
    /// <response code="400">Передан не корректный объект</response>
    /// <response code="401">Клиент не авторизован</response>
    /// <response code="403">Доступ запрещён</response>
    /// <response code="500">Произошла ошибка на сервере</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Car))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Car>> UpdateCar([FromBody] CarData carData, string id)
    {
        if (carData == null) throw new ValidationException("Передан пустой объект");

        CheckModelState();

        //TODO: Проверка на наличие у других авто переданного регистрационного номера и VIN

        var car = ConvertToModel(carData);

        car.Id = id;

        var result = await _carRepository.UpdateAsync(car);

        if (result == 0) throw new NotFoundException("Объект для изменения не найден");

        return Ok(car);
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <response code="204">Удаление успешно без возврата объекта</response>
    /// <response code="401">Клиент не авторизован</response>
    /// <response code="403">Доступ запрещён</response>
    /// <response code="500">Произошла ошибка на сервере</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult> DeleteCar(string id)
    {
        var result = await _carRepository.DeleteAsync(id);

        if (result == 0) throw new NotFoundException("Объект для удаления не найден");

        return NoContent();
    }

    /// <summary>
    /// Выборка автомобилей
    /// </summary>
    /// <param name="skip">Сколько пропустить</param>
    /// <param name="count">Сколько вывести</param>
    /// <response code="200">Успех</response>
    /// <response code="401">Клиент не авторизован</response>
    /// <response code="403">Доступ запрещён</response>
    /// <response code="500">Произошла ошибка на сервере</response>
    [HttpGet("limit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Car>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<IReadOnlyList<Car>>> GetByLimit([FromQuery] int skip, [FromQuery] int count) =>
        Ok(await _carRepository.GetByLimitAsync(skip, count));

    private Car ConvertToModel(CarData carData)
    {
        return new Car
        {
            Brand = carData.Brand,
            Lineup = carData.Lineup,
            Color = carData.Color,
            RegNumber = carData.RegNumber,
            Vin = carData.Vin
        };
    }
}