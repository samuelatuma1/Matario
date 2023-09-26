using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.DataAccess.AuthenticationModule
{
	public interface IRoleRepository : IBaseRepository<Role, long>
	{
		Task<bool> IsUniqueRoleName(string roleName);

		Task<Role?> GetRoleByName(string roleName);

		Task<Role?> FindByIdAndUpdate(Role model);
    }
}

