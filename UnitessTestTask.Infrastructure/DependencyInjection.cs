using Microsoft.Extensions.DependencyInjection;
using UnitessTestTask.Core.Interfaces;
using UnitessTestTask.Core.Interfaces.Repositories;
using UnitessTestTask.Infrastructure.Repositories;
using UnitessTestTask.Infrastructure.Security;

namespace UnitessTestTask.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICarRepository, CarRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}