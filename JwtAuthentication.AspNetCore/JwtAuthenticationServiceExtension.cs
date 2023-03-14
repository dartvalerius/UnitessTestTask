using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.AspNetCore;

public static class JwtAuthenticationServiceExtension
{
    /// <summary>
    /// Добавление сервиса аутентификации
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="tokenValidationParameters">Параметры валидации токена</param>
    /// <exception cref="ArgumentNullException">Если <param name="tokenValidationParameters"/> равен null</exception>
    public static void AddJwtAuthentication(this IServiceCollection services, TokenValidationParameters tokenValidationParameters)
    {
        if (tokenValidationParameters == null) throw new ArgumentNullException();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });
    }
}