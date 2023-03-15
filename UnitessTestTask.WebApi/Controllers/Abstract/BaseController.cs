using Microsoft.AspNetCore.Mvc;
using UnitessTestTask.WebApi.Exceptions;

namespace UnitessTestTask.WebApi.Controllers.Abstract;

public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Проверка состояния модели
    /// </summary>
    protected void CheckModelState()
    {
        if (ModelState.IsValid) return;

        var messages = string.Join(";\n", ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage));

        throw new ValidationException(messages);
    }
}