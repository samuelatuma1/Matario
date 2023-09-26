using System;
namespace Matario.Application.Contracts.Services.AuthenticationServiceModule
{
	public interface IRoleService
	{
		Task<bool> IsSuperAdmin(string jwtToken);
	}
}

