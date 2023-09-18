using System.Reflection;
using Matario.Application.Config;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Services.AuthenticationModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Matario.Application;
public static class ApplicationServicesCollection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HashConfig>(configuration.GetSection(nameof(HashConfig)));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Add Services
        services.AddScoped<IManageJwtService, ManageJwtService>();

        return services;
    }
}

