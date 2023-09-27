using Matario.Persistence.Config;
using Matario.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Persistence.DataAccess.Common;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Persistence.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.UoW;
using Matario.Persistence.UoW;
using Matario.Application.Contracts.DataAccess.OrganisationModule;
using Matario.Persistence.DataAccess.OrganisationModule;

namespace Matario.Persistence;
public static class PersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        // Replace with your connection string.
        var connectionString = configuration.GetSection(nameof(DbConfig)).Get<DbConfig>()?.ConnectionString;

        var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

        // Repositories
        services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository< , >));
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IOrganisationRepository, OrganisationRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
        return services;
    }
}

