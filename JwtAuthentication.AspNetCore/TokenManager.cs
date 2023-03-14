using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.AspNetCore;

/// <summary>
/// Менеджер токенов
/// </summary>
public class TokenManager
{
    private readonly JwtOptions _jwtOptions;

    public TokenManager(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    /// <summary>
    /// Сгенерировать токен
    /// </summary>
    /// <param name="claims">Утверждения</param>
    /// <returns>Строка токена</returns>
    public string? GenerateToken(IEnumerable<Claim>? claims = null)
    {
        if (string.IsNullOrEmpty(_jwtOptions.SecurityKey)) return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

        var securityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(_jwtOptions.ExpirationDays),
            new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    /// <summary>
    /// Получить параметры валидации токена
    /// </summary>
    /// <returns>Настроенный экземпляр класса <see cref="TokenValidationParameters"/></returns>
    public TokenValidationParameters? GetValidationParameters()
    {
        if (string.IsNullOrEmpty(_jwtOptions.SecurityKey)) return null;

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = !string.IsNullOrEmpty(_jwtOptions.Issuer),
            ValidIssuer = _jwtOptions.Issuer,
            ValidateAudience = !string.IsNullOrEmpty(_jwtOptions.Audience),
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SecurityKey)),
            ClockSkew = TimeSpan.Zero
        };
    }

    /// <summary>
    /// Проверка токена
    /// </summary>
    /// <param name="token">Строка токена</param>
    /// <returns><c>true</c> - если токен действительный</returns>
    public bool ValidateToken(string token)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            jwtSecurityTokenHandler.ValidateToken(token, GetValidationParameters(), out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}