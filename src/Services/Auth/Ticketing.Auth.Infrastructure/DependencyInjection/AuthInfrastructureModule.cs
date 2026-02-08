using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Auth.Infrastructure.Context;

namespace Ticketing.Auth.Infrastructure.DependencyInjection;

public static class AuthInfrastructureModule
{
    public static IServiceCollection AddAuthInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("AuthDb");
        services.AddDbContext<AuthDbContext>(opt => opt.UseNpgsql(cs));

        return services;
    }
}