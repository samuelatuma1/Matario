using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.DataAccess.AuthenticationModule
{
	public interface IAuthenticationRepository : IBaseRepository<User, long>
    {
        Task<bool> IsUniqueEmail(string email);
        Task<User?> FindByEmailAsync(string email);

        Task<bool> IsSuperAdmin(long userId);
    }
}

