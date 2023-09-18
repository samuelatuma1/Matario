using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.DataAccess.AuthenticationModule
{
	public interface IRefreshTokenRepository : IBaseRepository<RefreshToken, long>
	{
		
	}
}

