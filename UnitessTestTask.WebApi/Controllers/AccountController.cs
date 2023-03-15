using System.Security.Claims;
using JwtAuthentication.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using UnitessTestTask.Core.Entities;
using UnitessTestTask.Core.Enums;
using UnitessTestTask.Core.Interfaces;
using UnitessTestTask.Core.Interfaces.Repositories;
using UnitessTestTask.WebApi.Exceptions;
using UnitessTestTask.WebApi.Models;

namespace UnitessTestTask.WebApi.Controllers
{
    /// <summary>
    /// Работа с учётными записями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenManager _tokenManager;

        public AccountController(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            TokenManager tokenManager)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="regData">Данные регистрации пользователя</param>
        /// <returns>Идентификатор пользователя</returns>
        /// <response code="201">Пользователь создан</response>
        /// <response code="400">Переданы пустые или не валидные данные</response>
        /// <response code="500">Произошла ошибка на сервере</response>
        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegData regData)
        {
            CheckModelState(regData);

            var user = await _userRepository.GetByLoginAsync(regData.Login);
            if (user != null) throw new ValidationException("Пользователь с таким логином уже существует");

            var salt = _passwordHasher.GenerateSalt();

            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Login = regData.Login,
                Password = _passwordHasher.Hash(regData.Password, salt),
                Salt = salt,
                Role = UserRole.User.ToString()
            };

            await _userRepository.AddAsync(user);

            return Created("get", user.Id);
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="authData">Данные аутентификации пользователя</param>
        /// <returns>Идентификатор пользователя</returns>
        /// <response code="400">Переданы пустые или не валидные данные</response>
        /// <response code="401">Неправильные логин или пароль</response>
        /// <response code="500">Произошла ошибка на сервере</response>
        [HttpPost("authentication")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<string> AuthenticateAsync([FromBody] AuthData authData)
        {
            CheckModelState(authData);

            var user = await _userRepository.GetByLoginAsync(authData.Login);

            if (user == null || !_passwordHasher.IsValid(authData.Password, user.Password, user.Salt))
                throw new AuthException("Неправильные логин или пароль");

            var claims = new List<Claim>
            {
                new("Login", user.Login),
                new(ClaimTypes.Role, user.Role)
            };

            var token = _tokenManager.GenerateToken(claims);

            return token!;
        }

        /// <summary>
        /// Проверка состояния модели
        /// </summary>
        /// <param name="obj">Объект модели</param>
        private void CheckModelState(object obj)
        {
            if (obj == null) throw new ValidationException("Передан пустой объект");

            if (ModelState.IsValid) return;

            var messages = string.Join(";\n", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

            throw new ValidationException(messages);
        }
    }
}
