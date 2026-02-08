using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Auth.Application.Interface.Persistence;
using Ticketing.Auth.Application.Interface.Security;
using Ticketing.Auth.Infrastructure.Context;
using Ticketing.Auth.Infrastructure.Persistence.Repositories;
using Ticketing.Auth.Infrastructure.Security;

namespace Ticketing.Auth.Infrastructure.DependencyInjection;

public static class AuthInfrastructureModule
{
    public static void AddAuthInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("AuthDb");
        services.AddDbContext<AuthDbContext>(opt => opt.UseNpgsql(cs));
        
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, JwtTokenService>();
    }
}