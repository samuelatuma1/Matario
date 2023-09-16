using Matario.Application.Config;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Infrastructure.Services.AuthenticationServiceModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Matario.Infrastructure;
public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}

