using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.DataAccess.AuthenticationModule
{
	public interface IPermissionRepository : IBaseRepository<Permission, long>
	{
		Task<bool> IsUniquePermissionName(string permissionName);

		Task<Permission?> FindByIdAndUpdate(Permission model);

		Task<Permission?> GetPermissionByName(string name);

		Task<IEnumerable<Permission>> GetPermissionsForRoles(IEnumerable<long> roleIds);
    }
}

