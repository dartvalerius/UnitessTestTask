using System.Security.Cryptography;
using UnitessTestTask.Core.Interfaces;

namespace UnitessTestTask.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int Size = 96;
    private const int Iterations = 2;

    public bool IsValid(string password, string hash, string salt)
    {
        return Hash(password, salt).Equals(hash);
    }

    public string Hash(string password, string salt)
    {
        var rfc = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), Iterations,
            HashAlgorithmName.SHA256);

        var hash = rfc.GetBytes(Size);

        return Convert.ToBase64String(hash);
    }

    public string GenerateSalt()
    {
        var salt = new byte[Size];

        RandomNumberGenerator.Create().GetBytes(salt);

        return Convert.ToBase64String(salt);
    }
}